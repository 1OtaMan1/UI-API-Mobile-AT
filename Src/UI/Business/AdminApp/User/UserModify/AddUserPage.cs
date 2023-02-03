using Atata;

namespace UI.Business.AdminApp.User.UserModify;

using _ = AddUserPage;

public class AddUserPage : CommonUserModifyPage<_>
{
    [FindByClass("back-button")]
    public Button<UsersListPage, _> GoToUsersListButton { get; private set; }
}