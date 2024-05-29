namespace ReactiveBlazor.Client.Controls;

public partial class ErrorView
{
    private void TryAgainButtonClicked()
    {
        ViewModel?.Command.Execute(ViewModel.CommandParameter);
    }
}
