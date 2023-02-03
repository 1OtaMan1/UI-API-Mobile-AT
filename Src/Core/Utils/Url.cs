using static Core.EnvironmentSettings.ApplicationUrls;

namespace Core.Utils;

public static class Url
{
    public static string BaseAppUrl = Web;
    public static string ToAddCompany = BaseAppUrl + "/company-suggest";
    public static string ToAddNews = BaseAppUrl + "/news-submit";

    public static string AdminAppUrl = Admin;
    public static string ToUsersList = AdminAppUrl + "/user";
    public static string ToUserDetails(Guid userId) => ToUsersList + $"/{userId}";
}