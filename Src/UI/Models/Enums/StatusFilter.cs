using System.ComponentModel;

namespace UI.Models.Enums;

public enum StatusFilter
{
    [Description("All")]
    All,

    [Description("Blocked")]
    Disabled,

    [Description("Active")]
    Active
}