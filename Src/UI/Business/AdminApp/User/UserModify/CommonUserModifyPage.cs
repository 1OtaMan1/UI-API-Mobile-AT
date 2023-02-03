using Atata;
using UI.Atata;

namespace UI.Business.AdminApp.User.UserModify;

[PageObjectDefinition("article")]
public class CommonUserModifyPage<TOwner> : MainAdminAppPage<TOwner>
    where TOwner : CommonUserModifyPage<TOwner>
{
    [FindByXPath("label[contains(text(), 'Email')]/parent::div[@class='form-group']//input")]
    public CustomEditableTextField<TOwner> EmailInput { get; private set; }

    [FindByXPath("label[contains(text(), 'Name')]/parent::div[@class='form-group']//input")]
    public CustomEditableTextField<TOwner> FirstNameInput { get; private set; }

    [FindByXPath("label[contains(text(), 'Surname')]/parent::div[@class='form-group']//input")]
    public CustomEditableTextField<TOwner> LastNameInput { get; private set; }

    [FindByXPath("label[text()='Block this user']/parent::div[@class='form-group']")]
    public CheckBox<TOwner> IsDisabled { get; private set; }

    [FindByClass("add-edit-button")]
    public Button<UserDetailsPage, TOwner> SubmitButton { get; private set; }
}