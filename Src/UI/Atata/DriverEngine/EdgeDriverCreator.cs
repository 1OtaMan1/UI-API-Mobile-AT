using Atata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using LogLevel = Atata.LogLevel;

namespace UI.Atata.DriverEngine;

public class EdgeDriverCreator : IDriverCreator
{
    public AtataContext CreateWebDriver(TestContext context)
    {
        var findTimeout = context.Properties["ElementFindTimeout"] as string;
        var retryTimeout = context.Properties["BaseRetryTimeout"] as string;
        var retryInterval = context.Properties["BaseRetryInterval"] as string;
        var options = new EdgeOptions { UnhandledPromptBehavior = UnhandledPromptBehavior.Accept };
        options.AddAdditionalOption("InPrivate", true);

        AtataContext.Configure().
            UseEdge().
            WithOptions(options).
            AddTraceLogging().
            WithMinLevel(LogLevel.Info).
            UseElementFindTimeout(TimeSpan.FromMilliseconds(double.Parse(findTimeout))).
            UseBaseRetryTimeout(TimeSpan.FromMilliseconds(double.Parse(retryTimeout))).
            UseBaseRetryInterval(TimeSpan.FromMilliseconds(double.Parse(retryInterval))).
            Build()
            .Driver.Maximize();

        AtataContext.Current.Driver.Maximize();

        return AtataContext.Current;
    }
}