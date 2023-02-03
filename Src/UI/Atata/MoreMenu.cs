using Atata;

#pragma warning disable 693

namespace UI.Atata;

[ControlDefinition("app-more-menu-button", ComponentTypeName = "menu", IgnoreNameEndings = "menu, Menu")]
[WaitForPageLoad(TriggerEvents.BeforeAnyAction)]
[ClickParent(AppliesTo = TriggerScope.Children)]
public class MoreMenu<TOwner> : Clickable<TOwner>
    where TOwner : PageObject<TOwner>
{
    [HoverParent(AppliesTo = TriggerScope.Children)]
    public class DropdownOption<TOwner> : Control<TOwner> 
        where TOwner : PageObject<TOwner>
    {

    }
}