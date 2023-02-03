using Atata;
using UI.Business.AdminApp.Common.Modals;
using UI.Business.AdminApp.User.UserModify;

namespace UI.Business.AdminApp.User;

using _ = UserDetailsPage;

[PageObjectDefinition("article")]
public class UserDetailsPage : MainAdminAppPage<_>
{
    [FindByClass("back-button")]
    public Button<UsersListPage, _> GoToUsersListButton { get; private set; }

    public UserDetailsContainer UserDetails { get; private set; }

    [ControlDefinition("div", ContainingClass = "card-body")]
    public class UserDetailsContainer : Control<_>
    {
        [FindByXPath("b[text()='Email']/parent::h6/span[@class='details-text']")]
        public Text<_> Email { get; private set; }

        [FindByXPath("b[text()='Name']/parent::h6/span[@class='details-text']")]
        public Text<_> FirstName { get; private set; }

        [FindByXPath("b[text()='Surname']/parent::h6/span[@class='details-text']")]
        public Text<_> LastName { get; private set; }

        [FindByXPath("b[text()='Status']/parent::h6/span[@class='red-text' or @class='green-text']")]
        public Text<_> Status { get; private set; }

        [FindByXPath("b[text()='Created']/parent::h6")]
        public Text<_> CreatedDate { get; private set; }
    }

    public ActionButtonsContainer ActionButtons { get; private set; }

    [ControlDefinition("div", ContainingClass = "details-buttons")]
    public class ActionButtonsContainer : Control<_>
    {
        [FindByContent("Edit")]
        public Button<EditUserPage, _> EditUserButton { get; private set; }

        [FindByContent("Block")]
        public Button<_, _> BlockUserButton { get; private set; }

        [FindByContent("Assign all data to admin")]
        public Button<_, _> AssignAllToAdminButton { get; private set; }

        [FindByContent("Delete user news")]
        public Button<ConfirmationModal<_>, _> DeleteNewsButton { get; private set; }

        [FindByContent("Delete user companies")]
        public Button<ConfirmationModal<_>, _> DeleteCompaniesButton { get; private set; }

        [FindByContent("Delete user")]
        public Button<ConfirmationModal<_>, _> DeleteUserButton { get; private set; }
    }

    public ControlList<Company, _> Companies { get; private set; }

    [ControlDefinition("th[contains(text(), 'Logo')]/ancestor::table[1]/tbody/tr")]
    public class Company : Control<_>
    {
        [FindByCss("td.media")]
        public Clickable<_, _> Logo { get; private set; }

        [FindByCss("td[2]")]
        public Clickable<_, _> Name { get; private set; }

        [FindByXPath("td[3]")]
        public Text<_> CeoName { get; private set; }

        [FindByXPath("td[4]")]
        public Text<_> Statistic { get; private set; }

        [FindByCss("td.link-cell")]
        public Clickable<_, _> Link { get; private set; }

        [FindByXPath("td[6]")]
        public Text<_> CreatedDate { get; private set; }

        [FindByXPath("td[7]")]
        public Text<_> IsConfirmed { get; private set; }

        [FindByXPath("td[8]")]
        public Text<_> IsVerified { get; private set; }

        [FindByXPath("td[9]")]
        public Text<_> IsStillWorking { get; private set; }
    }

    public ControlList<NewsItem, _> News { get; private set; }

    [ControlDefinition("th[contains(text(), 'Company name')]/ancestor::table[1]/tbody/tr")]
    public class NewsItem : Control<_>
    {
        [FindByCss("td.name-cell")]
        public Clickable<_, _> Name { get; private set; }

        [FindByXPath("td[2]")]
        public Text<_> Title { get; private set; }

        [FindByXPath("td[3]")]
        public Text<_> CreatedDate { get; private set; }

        [FindByXPath("td[4]")]
        public Text<_> IsConfirmed { get; private set; }

        [FindByXPath("td[5]")]
        public Text<_> IsVerified { get; private set; }

        [FindByCss("td.link-cell")]
        public Clickable<_, _> Link { get; private set; }
    }

    public Company GetCompany(string companyName) => Companies[cn => cn.Name.Content.Value.Contains(companyName)];

    public NewsItem GetNewsItem(string url) => News[ni => ni.Link.Content.Value.Contains(url)];
}