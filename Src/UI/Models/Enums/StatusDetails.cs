using System.ComponentModel;

namespace UI.Models.Enums;

public enum StatusDetails
{
    [Description("Blocked")]
    Disabled,

    [Description("Active")]
    Enabled
}