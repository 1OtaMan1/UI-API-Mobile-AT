using Atata;
using UI.Atata;
using UI.Business.AdminApp.User;

namespace UI.Business.AdminApp;

[WaitForPageLoad(TriggerEvents.BeforeAnyAction)]
public class MainAdminAppPage<TOwner> : Page<TOwner>
    where TOwner : MainAdminAppPage<TOwner>
{
    public SideBarContent SideBar { get; private set; }

    [ControlDefinition("div", ContainingClass = "sidebar")]
    public class SideBarContent : Control<TOwner>
    {
        [FindByClass("navbar-brand")]
        public Clickable<TOwner, TOwner> HomeLink { get; private set; }
    }

    [FindByXPath("article/*[local-name()='h1' or local-name()='h3']")]
    public Text<TOwner> Title { get; private set; }

    public NavigationTabsContent NavigationTabs { get; private set; }

    [ControlDefinition("nav")]
    public class NavigationTabsContent : Control<TOwner>
    {
        [FindByCss("a[href='user']")]
        public Clickable<UsersListPage, TOwner> UsersLink { get; private set; }
    }
}