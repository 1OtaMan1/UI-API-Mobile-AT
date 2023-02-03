using System.Diagnostics;
using Atata;
using Polly;

namespace UI.Atata.Extensions;

public static class GoWithRetry
{
    public static T To<T>(T pageObject = null, string url = null, bool navigate = true, bool temporarily = false)
        where T : PageObject<T>
    {
        T page = null;

        var hasErrors = Policy
            .HandleResult<bool>(result => result)
            .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(2))
            .Execute(() =>
            {
                page = Go.To(pageObject, url, navigate, temporarily);

                var pageUnavailable = AtataContext.Current.Driver.FindElementsById("PageUnavailable").Count > 0;
                var pageNotFound = AtataContext.Current.Driver.FindElementsById("HTTP404").Count > 0;

                if (pageUnavailable || pageNotFound)
                {
                    Trace.TraceError("Got issues with page loading, trying again...");
                    return true;
                }

                return false;
            });

        if (hasErrors)
        {
#pragma warning disable CA2201 // Do not raise reserved exception types
            throw new Exception("Failed to open page");
        }
#pragma warning restore CA2201 // Do not raise reserved exception types

        return page;
    }
}