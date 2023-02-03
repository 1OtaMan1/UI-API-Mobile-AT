using Atata;

namespace UI.Atata;

[ControlDefinition("app-dropdown-menu", ComponentTypeName = "dropdown", IgnoreNameEndings = "Dropdown")]
[ClickParent(AppliesTo = TriggerScope.Children)]
public class DropdownMenu<TOwner> : Clickable<TOwner>
    where TOwner : PageObject<TOwner>
{
}