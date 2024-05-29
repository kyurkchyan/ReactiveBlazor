using System.ComponentModel;

namespace ReactiveBlazor.ViewModels.Common.ViewModels;

public enum Severity
{
    [Description("normal")] Normal,
    [Description("info")] Info,
    [Description("success")] Success,
    [Description("warning")] Warning,
    [Description("error")] Error
}
