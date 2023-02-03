using Atata;

namespace UI.Atata;

[ControlDefinition("div", ContainingClass = "edit-menu-container")]
[WaitForPageLoad(TriggerEvents.BeforeAnyAction)]
[ClickParent(AppliesTo = TriggerScope.Children)]
public class PenIconMenu<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
{
}