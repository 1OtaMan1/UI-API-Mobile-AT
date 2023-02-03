using Atata;
using Core.EnvironmentSettings;
using UI.Atata;
using UI.Atata.Extensions;
using UI.Business.AdminApp.Dashboard;

namespace UI.Business.AdminApp;

using _ = AdminLoginPage;

[PageObjectDefinition("article")]
public class AdminLoginPage : MainAdminAppPage<_>
{
    [FindByXPath("label[contains(text(), 'Username')]/parent::div[@class='form-group']//input")]
    public CustomEditableTextField<_> LoginInput { get; private set; }

    [FindByXPath("label[contains(text(), 'Password')]/parent::div[@class='form-group']//input")]
    public CustomEditableTextField<_> PasswordInput { get; private set; }

    [FindByContent("Log in")]
    public Button<DashboardPage, _> SignInButton { get; private set; }

    public T LoginTo<T>(string url, CredentialsStorage userCredential) where T : Page<T>
    {
        LoginInput.Wait(Until.Visible);
        RefreshPage();
        LoginInput.Wait(Until.Visible);

        LoginInput.Set(userCredential.Login);
        PasswordInput.Set(userCredential.Password);
        var companiesListPage = SignInButton.ClickAndGo();
        companiesListPage.Chart.Wait(Until.Visible);

        var page = GoWithRetry.To<T>(url: url);
        return page;
    }
}