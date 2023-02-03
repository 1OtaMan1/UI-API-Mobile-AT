using Atata;
using Core.EnvironmentSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static UI.Atata.DriverEngine.WebDriverTypes;

namespace UI.Atata.DriverEngine;

public static class DriverFactory
{
    public static AtataContext Context;

    public static AtataContext CreateDriverInstance(TestContext context)
    {
        Context = null;
        var driverType = context.Properties["BrowserType"] as string;

        switch (driverType?.ToLower())
        {
            case Firefox:
                Context = new FirefoxDriverCreator().CreateWebDriver(context);
                break;
            case Chrome:
                Context = ClientsConfiguration.LocalMachines.Any(lm => lm.Equals(Environment.MachineName))
                    ? new ChromeDriverCreator().CreateWebDriver(context)
                    : new ChromeHeadlessDriverCreator().CreateWebDriver(context);
                break;
            case Ie:
                Context = new InternetExplorerDriverCreator().CreateWebDriver(context);
                break;
            case Edge:
                Context = new EdgeDriverCreator().CreateWebDriver(context);
                break;
            case ChromeHeadless:
                Context = new ChromeHeadlessDriverCreator().CreateWebDriver(context);
                break;
            case FirefoxHeadless:
                Context = new FirefoxHeadlessDriverCreator().CreateWebDriver(context);
                break;
            default:
                AtataContext.Current.Log.Warn("Warning: ***Browser type is incorrect. Starting chrome browser as default***");
                Context = new ChromeDriverCreator().CreateWebDriver(context);
                break;
        }

        return Context;
    }
}