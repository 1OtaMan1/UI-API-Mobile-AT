using Atata;

namespace UI.Business.AdminApp.User.UserModify;

using _ = EditUserPage;

public class EditUserPage : CommonUserModifyPage<_>
{
    [FindByClass("back-button")]
    public Button<UserDetailsPage, _> GoToUserDetailsButton { get; private set; }
}