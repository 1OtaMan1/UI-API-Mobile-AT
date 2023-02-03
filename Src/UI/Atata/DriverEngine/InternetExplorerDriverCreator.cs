using Atata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using LogLevel = Atata.LogLevel;

namespace UI.Atata.DriverEngine;

public class InternetExplorerDriverCreator : IDriverCreator
{
    public AtataContext CreateWebDriver(TestContext context)
    {
        var findTimeout = context.Properties["ElementFindTimeout"] as string;
        var retryTimeout = context.Properties["BaseRetryTimeout"] as string;
        var retryInterval = context.Properties["BaseRetryInterval"] as string;
        var options = new InternetExplorerOptions { UnhandledPromptBehavior = UnhandledPromptBehavior.Accept };

        AtataContext.Configure().
            UseInternetExplorer().
            WithOptions(options).
            AddTraceLogging().WithMinLevel(LogLevel.Info).
            UseElementFindTimeout(TimeSpan.FromMilliseconds(double.Parse(findTimeout))).
            UseBaseRetryTimeout(TimeSpan.FromMilliseconds(double.Parse(retryTimeout))).
            UseBaseRetryInterval(TimeSpan.FromMilliseconds(double.Parse(retryInterval))).
            Build();

        AtataContext.Current.Driver.Maximize();

        return AtataContext.Current;
    }
}