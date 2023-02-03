using Atata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using LogLevel = Atata.LogLevel;

namespace UI.Atata.DriverEngine;

public class FirefoxHeadlessDriverCreator : IDriverCreator
{
    public AtataContext CreateWebDriver(TestContext context)
    {
        var findTimeout = context.Properties["ElementFindTimeout"] as string;
        var retryTimeout = context.Properties["BaseRetryTimeout"] as string;
        var retryInterval = context.Properties["BaseRetryInterval"] as string;

        var options = new FirefoxOptions {UnhandledPromptBehavior = UnhandledPromptBehavior.Accept};
        options.AddArguments("--private, --headless", "--disable-gpu", "--hide-scrollbars");

        AtataContext.Configure().
            UseFirefox().
            WithOptions(options).
            AddTraceLogging().
            WithMinLevel(LogLevel.Info).
            UseElementFindTimeout(TimeSpan.FromMilliseconds(double.Parse(findTimeout))).
            UseBaseRetryTimeout(TimeSpan.FromMilliseconds(double.Parse(retryTimeout))).
            UseBaseRetryInterval(TimeSpan.FromMilliseconds(double.Parse(retryInterval))).
            Build();

        AtataContext.Current.Driver.SetSize(1980, 838);

        return AtataContext.Current;
    }
}