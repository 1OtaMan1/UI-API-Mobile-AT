using Atata;

namespace UI.Atata;

/// <summary>
/// After certain actions, we expect to see Success Alert.
/// Trigger priority is set to High to be executed before WaitForPageLoad.
/// </summary>
public class WaitForSuccessAlertAttribute : WaitForElementAttribute
{
    public WaitForSuccessAlertAttribute(
        WaitBy waitBy = WaitBy.Css, 
        string selector = "snack-bar-container", 
        Until until = Until.VisibleThenMissing,
        TriggerEvents @on = TriggerEvents.AfterClick, 
        TriggerPriority priority = TriggerPriority.High) : base(waitBy, selector, until, @on, priority)
    {
        ScopeSource = ScopeSource.Page;
    }
}