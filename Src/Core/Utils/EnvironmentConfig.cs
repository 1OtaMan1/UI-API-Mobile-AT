using OpenQA.Selenium;

namespace Core.Utils;

public static class EnvironmentConfig
{
    public static string GetEnvironmentBaseInfo()
    {
        var screenHeight = 740; ////Screen.PrimaryScreen.Bounds.Height.ToString();
        var screenWidth = 1280; ////Screen.PrimaryScreen.Bounds.Width.ToString();

        return "\n\n" +
               "VM name: " + Environment.MachineName + "\n" +
               "VM User: " + Environment.UserName + "\n" +
               "OS Version: " + Environment.OSVersion + "\n" +
               "Screen Resolution: " + screenWidth + "x" + screenHeight + "\n";
    }

    public static string GetBrowserDetails(IWebDriver browserInstance)
    {
        var capabilities = ((WebDriver)browserInstance).Capabilities;
        var browserVersion = capabilities.GetCapability("browserName").Equals("chrome")
            ? capabilities.GetCapability("version")
            : capabilities.GetCapability("browserVersion");
        return "\n\n" +
               "Browser Type: " + capabilities.GetCapability("browserName") + "\n" +
               "Browser Version: " + browserVersion + "\n" +
               "Browser Resolution: " + browserInstance.Manage().Window.Size.Width + "x" + browserInstance.Manage().Window.Size.Height + "\n";
    }
}