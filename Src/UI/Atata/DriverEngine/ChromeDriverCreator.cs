using Atata;
using Core.EnvironmentSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using LogLevel = Atata.LogLevel;

namespace UI.Atata.DriverEngine;

public class ChromeDriverCreator : IDriverCreator
{
    private static readonly string Environment = ConfigurationManager.AppSettings["Environment"];

    public AtataContext CreateWebDriver(TestContext context)
    {
        var findTimeout = context.Properties["ElementFindTimeout"] as string;
        var retryTimeout = context.Properties["BaseRetryTimeout"] as string;
        var retryInterval = context.Properties["BaseRetryInterval"] as string;

        var options = new ChromeOptions { UnhandledPromptBehavior = UnhandledPromptBehavior.Accept };
        options.AddArgument("--incognito");
        if (Environment != "prod")
        {
            options.AddUserProfilePreference("download.default_directory", AtataUtils.CurrentDirectory);
        }

        if (Environment == "prod")
        {
            options.AddArgument("--whitelisted-ips=127.0.0.1");
        }

        var builder = AtataContext.Configure()
            .UseChrome()
            .WithArguments("window-size=1920,838")
            .WithOptions(options)
            .AddTraceLogging()
            .WithMinLevel(LogLevel.Info)
            .UseElementFindTimeout(TimeSpan.FromMilliseconds(double.Parse(findTimeout)))
            .UseBaseRetryTimeout(TimeSpan.FromMilliseconds(double.Parse(retryTimeout)))
            .UseBaseRetryInterval(TimeSpan.FromMilliseconds(double.Parse(retryInterval)));

        return builder.Build();
    }
}