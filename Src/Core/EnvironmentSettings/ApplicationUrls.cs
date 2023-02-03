namespace Core.EnvironmentSettings;

public static class ApplicationUrls
{
    public static readonly string Api = ConfigurationManager.AppSettings["BaseUrl:TenantAPI"];
    public static readonly string Web = ConfigurationManager.AppSettings["BaseUrl:TenantUI"];
    public static readonly string Admin = ConfigurationManager.AppSettings["BaseUrl:AdminUI"];
}