using Atata;
using Core.EnvironmentSettings;
using UI.Business.AdminApp;

namespace UI.Atata.Extensions;

public static class LoginAndGo
{
    public static TPage To<TPage>(string url, CredentialsStorage credentials)
        where TPage : Page<TPage>
    {
        return GoWithRetry.To<AdminLoginPage>(url: url).LoginTo<TPage>(url, credentials);
    }
}