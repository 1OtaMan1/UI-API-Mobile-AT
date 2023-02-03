namespace Core.Utils;

public static class DateTimeUtils
{
    public static string GetCurrentTime()
    {
        return DateTime.UtcNow.ToString("HH:mm:ss:ffff");
    }

    public static DateTime GetDate()
    {
        return DateTime.UtcNow.Date;
    }
}