namespace ReactiveBlazor.Client.ReactiveUI.Forms;

public interface IReactiveField
{
    public string? ValidationError { get; set; }
    public bool Error { get; set; }
    public string? ErrorText { get; set; }
}