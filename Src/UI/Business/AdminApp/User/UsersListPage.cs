using Atata;
using UI.Atata;
using UI.Business.AdminApp.Common.Sections;
using UI.Business.AdminApp.User.UserModify;

namespace UI.Business.AdminApp.User;

using _ = UsersListPage;

[PageObjectDefinition("article")]
public class UsersListPage : MainAdminAppPage<_>
{
    [FindByContent("Add user")]
    public Button<AddUserPage, _> AddUserButton { get; private set; }

    public Filter<_> Filter { get; private set; }

    public OrderByContainer OrderBy { get; private set; }

    [WaitForPageLoad(TriggerEvents.BeforeAndAfterClick, TriggerPriority.High, TargetAllChildren = true)]
    [ControlDefinition("thead//tr")]
    public class OrderByContainer : Control<_>
    {
        [FindByXPath("th[1]")]
        public Clickable<_, _> Email { get; private set; }

        [FindByXPath("th[2]")]
        public Clickable<_, _> Name { get; private set; }

        [FindByXPath("th[3]")]
        public Clickable<_, _> Surname { get; private set; }

        [FindByXPath("th[4]")]
        public Clickable<_, _> CreatedDate { get; private set; }

        [FindByXPath("th[5]")]
        public Clickable<_, _> IsBlocked { get; private set; }
    }

    public ControlList<User, _> Users { get; private set; }

    [ControlDefinition("tbody//tr")]
    public class User : Control<_>
    {
        [FindByXPath("td[1]")]
        public Clickable<UserDetailsPage, _> Email { get; private set; }

        [FindByXPath("td[2]")]
        public Clickable<UserDetailsPage, _> Name { get; private set; }

        [FindByXPath("td[3]")]
        public Clickable<UserDetailsPage, _> Surname { get; private set; }

        [FindByXPath("td[4]")]
        public Clickable<UserDetailsPage, _> CreatedDate { get; private set; }

        [FindByXPath("td[5]")]
        public Clickable<UserDetailsPage, _> IsDisabled { get; private set; }
    }

    public User GetUser(string email) => Users[user => user.Email.Content.Value.Contains(email)];
}