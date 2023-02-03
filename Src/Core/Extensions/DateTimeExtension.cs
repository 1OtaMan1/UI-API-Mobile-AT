namespace Core.Extensions;

public static class DateTimeExtension
{
    const int Second = 1;
    const int Minute = 60 * Second;
    const int Hour = 60 * Minute;
    const int Day = 24 * Hour;
    const int Month = 30 * Day;

    /// <summary>
    /// To the relative date.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static string ToRelativeDate(this DateTime dateTime)
    {
        var ts = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks);
        var delta = Math.Abs(ts.TotalSeconds);

        // Consider less than 5 seconds as "just now"
        // and 6-30 seconds as "Few seconds ago"
        if (delta < 1 * Minute)
        {
            if (ts.Seconds <= 5)
            {
                return "Just now";
            }
            else
            {
                return ts.Seconds + " seconds ago";
            }
        }

        if (delta < 2 * Minute)
        {
            return "1 minute ago";
        }

        if (delta < 45 * Minute)
        {
            return ts.Minutes + " minutes ago";
        }

        if (delta < 2 * Hour)
        {
            return "1 hour ago";
        }

        if (delta < 24 * Hour)
        {
            return ts.Hours + " hours ago";
        }

        if (delta < 48 * Hour)
        {
            return "yesterday";
        }

        if (delta < 30 * Day)
        {
            return ts.Days + " days ago";
        }

        if (delta < 12 * Month)
        {
            var months = Convert.ToInt32(Math.Round((double)ts.Days / 30));
            return months <= 1 ? "1 month ago" : months + " months ago";
        }
        else
        {
            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "1 year ago" : years + " years ago";
        }
    }
    public static string MonthName(this DateTime dateTime)
    {
        var monthNumber = dateTime.Month;
        return monthNumber switch
        {
            1 => ("January"),
            2 => ("February"),
            3 => ("March"),
            4 => ("April"),
            5 => ("May"),
            6 => ("June"),
            7 => ("July"),
            8 => ("August"),
            9 => ("September"),
            10 => ("October"),
            11 => ("November"),
            12 => ("December"),
            _ => ("January")
        };
    }
}