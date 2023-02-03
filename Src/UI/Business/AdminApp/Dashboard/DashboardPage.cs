using Atata;

namespace UI.Business.AdminApp.Dashboard;

using _ = DashboardPage;

[PageObjectDefinition("article")]
public class DashboardPage : MainAdminAppPage<_>
{
    [FindByXPath("*[contains(@class, 'chart')]")]
    public Control<_> Chart { get; private set; }
}