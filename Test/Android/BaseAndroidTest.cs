using System.Diagnostics;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Tests.UI;

[TestClass]
public class BaseAndroidTest
{
    public TestContext TestContext { get; set; }
    public AndroidDriver<AppiumWebElement> driver = GetDriver();

    public static AndroidDriver<AppiumWebElement> GetDriver()
    {
        var options = new AppiumOptions();
        options.PlatformName = "Android";
        options.AddAdditionalCapability("deviceName", "sdk_gpphone_x86");
        options.AddAdditionalCapability("platformVersion", "11");

        return new AndroidDriver<AppiumWebElement>(new Uri("http://127.0.0.1:4723/wd/hub"), options);
    }

    [TestInitialize]
    public void StartScreenRecord()
    {
        driver.StartRecordingScreen();
    }

    [TestCleanup]
    public void TearDown()
    {
        var testName = $"{TestContext.TestName}";
        var currentTestOutcome = TestContext.CurrentTestOutcome;
        var message = $"\nEnd time: {DateTime.Now}" +
                      $"\nTest name: '{testName}'" +
                      $"\nStatus: {currentTestOutcome.ToString().ToUpperInvariant()}\n";
        if (currentTestOutcome != UnitTestOutcome.Passed)
        {
            Trace.TraceError(message);

            var video = driver.StopRecordingScreen();
            byte[] ret = Convert.FromBase64String(video);
            FileInfo file = new($"H:\\videoRecords\\{DateTime.Now.ToString("yyyyMMddTHHmmss")}.mp4");
            using Stream sw = file.OpenWrite();
            sw.Write(ret, 0, ret.Length);
            sw.Close();
        }
        else
        {
            Trace.TraceInformation(message);
        }
    }
}