using Atata;
using UI.Atata;

namespace UI.Business.AdminApp.Common.Modals;

[WaitForPageLoad(TriggerEvents.BeforeAnyAction)]
[PageObjectDefinition("div", ContainingClass = "modal")]
public class ConfirmationModal<TOwner> : PageObject<ConfirmationModal<TOwner>>
    where TOwner : PageObject<TOwner>
{
    [FindByClass("modal-title")]
    public Text<ConfirmationModal<TOwner>> Title { get; private set; }

    [FindByClass("modal-body")]
    public Text<ConfirmationModal<TOwner>> Description { get; private set; }

    [WaitFor]
    [FindByClass("btn-danger")]
    public Clickable<TOwner, ConfirmationModal<TOwner>> SubmitButton { get; private set; }

    [FindByClass("btn-secondary")]
    public Clickable<TOwner, ConfirmationModal<TOwner>> CancelButton { get; private set; }
}