using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Concurrency;
using System.Reactive.Threading.Tasks;
using DynamicData;
using FluentValidation;
using FluentValidation.Internal;
using ReactiveUI.Validation.Collections;
using ReactiveUI.Validation.Components.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.States;

namespace ReactiveBlazor.ViewModels.Common.Validation;

public class AbstractViewModelValidator<TViewModel> : AbstractValidator<TViewModel>, IViewModelValidator
    where TViewModel : IReactiveObject, IValidatableViewModel, ReactiveUI.Validation.Abstractions.IValidatableViewModel
{
    private readonly TViewModel _viewModel;
    private readonly HashSet<string> _mentionedPropertyNames = new();
    private readonly IDisposable _bindings;
    private readonly ValidationContext _validationContext;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AbstractViewModelValidator{T}" /> class.
    /// </summary>
    /// <param name="viewModel">The view model that should be validated</param>
    /// <param name="scheduler">
    ///     Scheduler for the <see cref="ValidationContext" />. Uses
    ///     <see cref="CurrentThreadScheduler" /> by default.
    /// </param>
    protected AbstractViewModelValidator(TViewModel viewModel,
        IScheduler? scheduler = null)
    {
        _viewModel = viewModel;

        _validationContext = new ValidationContext(scheduler);
        _viewModel.SetValidationContext(_validationContext);
        _bindings = _validationContext.Validations
            .Connect()
            .ToCollection()
            .Select(components => components
                .Select(component => component
                    .ValidationStatusChange
                    .Select(_ => component))
                .Merge()
                .StartWith(_validationContext))
            .Switch()
            .Subscribe(OnValidationStatusChange);
    }

    public IObservable<bool> Valid => _validationContext.Valid;

    protected void RuleFor<TProperty>(Expression<Func<TViewModel, TProperty>> expression,
        Action<IRuleBuilder<TViewModel, TProperty>> configureRules)
    {
        IRuleBuilder<TViewModel, TProperty> ruleBuilderInitial = RuleFor(expression);

        configureRules.Invoke(ruleBuilderInitial);

        _viewModel.ValidationRule(expression, GetPropertyValidation(expression));
    }

    public virtual IEnumerable GetErrors(string? propertyName) =>
        string.IsNullOrWhiteSpace(propertyName)
            ? GetAllErrors()
            : GetPropertyErrors(propertyName);

    public string[] GetAllErrors()
        => SelectInvalidPropertyValidations()
            .SelectMany(state => state.Text ?? ValidationText.None)
            .ToArray();

    public string[] GetPropertyErrors(string propertyName)
        => SelectInvalidPropertyValidations()
            .Where(validation => validation.ContainsPropertyName(propertyName))
            .SelectMany(state => state.Text ?? ValidationText.None)
            .ToArray();

    /// <summary>
    ///     Raises the <see cref="INotifyDataErrorInfo.ErrorsChanged" /> event.
    /// </summary>
    /// <param name="propertyName">The name of the validated property.</param>
    private void RaiseErrorsChanged(string propertyName = "") =>
        _viewModel.RaiseErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));

    /// <summary>
    ///     Selects validation components that are invalid.
    /// </summary>
    /// <returns>Returns the invalid property validations.</returns>
    private IEnumerable<IPropertyValidationComponent> SelectInvalidPropertyValidations() =>
        _validationContext.Validations.Items
            .OfType<IPropertyValidationComponent>()
            .Where(validation => !validation.IsValid);

    /// <summary>
    ///     Updates the <see cref="INotifyDataErrorInfo.HasErrors" /> property before raising the
    ///     <see cref="INotifyDataErrorInfo.ErrorsChanged" />
    ///     event, and then raises the <see cref="INotifyDataErrorInfo.ErrorsChanged" /> event. This behaviour is required by
    ///     WPF, see:
    ///     https://stackoverflow.com/questions/24518520/ui-not-calling-inotifydataerrorinfo-geterrors/24837028.
    /// </summary>
    /// <remarks>
    ///     WPF doesn't understand string.Empty as an argument for the <see cref="INotifyDataErrorInfo.ErrorsChanged" />
    ///     event, so we are sending <see cref="INotifyDataErrorInfo.ErrorsChanged" /> notifications for every saved property.
    ///     This is required for e.g. cases when a <see cref="IValidationComponent" /> is disposed and
    ///     detached from the <see cref="_validationContext" />, and we'd like to mark all invalid
    ///     properties as valid (because the thing that validates them no longer exists).
    /// </remarks>
    private void OnValidationStatusChange(IValidationComponent component)
    {
        _viewModel.SetHasErrors(!_validationContext.GetIsValid());
        if (component is IPropertyValidationComponent propertyValidationComponent)
        {
            foreach (var propertyName in propertyValidationComponent.Properties)
            {
                RaiseErrorsChanged(propertyName);
                _mentionedPropertyNames.Add(propertyName);
            }
        }
        else
        {
            foreach (var propertyName in _mentionedPropertyNames)
            {
                RaiseErrorsChanged(propertyName);
            }
        }
    }

    private IObservable<IValidationState> GetPropertyValidation<TProperty>(
        Expression<Func<TViewModel, TProperty>> expression)
    {
        return _viewModel.WhenAnyValue(expression)
            .Select(_ => GetValidationContextForProperty(expression))
            .SelectMany(context => ValidateAsync(context).ToObservable())
            .Select(validationResult => new FluentValidationState(validationResult));
    }

    private ValidationContext<TViewModel> GetValidationContextForProperty<TProperty>(
        Expression<Func<TViewModel, TProperty>> expression)
    {
        string[] properties = [((MemberExpression)expression.Body).Member.Name];
        var context = new ValidationContext<TViewModel>(_viewModel, new PropertyChain(),
            new MemberNameValidatorSelector(properties));
        return context;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _validationContext.Dispose();
            _bindings.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}