using System;
using System.Diagnostics;
using System.Linq;
using Atata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using API.ApiUsers;
using Core.EnvironmentSettings;
using Core.Utils;
using UI.Atata.DriverEngine;
using Atata.ExtentReports;

namespace Tests.UI;

[TestClass]
public class BaseUiTest
{
    protected static WaitOptions WaitTimeout;

    protected static string DriverProcessName;
    protected static string BrowserProcessName;

    protected static TenantApiUser ContributorMailtrap = new(ApiRoles.ContributorMailtrap);
    protected static TenantApiUser SelfSignedContributorMailtrap = new(ApiRoles.SelfSignedContributorMailtrap);
    protected static TenantApiUser DisabledContributorMailtrap = new(ApiRoles.DisabledContributorMailtrap);
    protected static TenantApiUser Admin = new(ApiRoles.Admin);

    public TestContext TestContext { get; set; }

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        WaitTimeout = new WaitOptions(int.Parse(context.Properties["WaitTimeout"].ToString()));
    }

    [TestInitialize]
    public void SetUp()
    {
        CloseBrowsers();

        AtataContext.Configure()
            .UseDriverInitializationStage(AtataContextDriverInitializationStage.OnDemand)
            .AddLogConsumer<ExtentLogConsumer>()
            .Build();

        DriverFactory.CreateDriverInstance(TestContext);
        AtataContext.Current?.Log.Trace(EnvironmentConfig.GetEnvironmentBaseInfo() +
                                        EnvironmentConfig.GetBrowserDetails(AtataContext.Current?.Driver) + "\n" +
                                        "*****Executing test with name: " + TestContext.TestName + "*****\n" +
                                        "________________________________________________________________\n");

        DriverProcessName = GetDriverProcessName();
        BrowserProcessName = GetBrowserProcessName();
    }

    [TestCleanup]
    public void TearDown()
    {
        var testName = $"{TestContext.TestName}";
        var currentTestOutcome = TestContext.CurrentTestOutcome;
        var message = $"\n*****Test name: '{testName}'" + "*****" +
                      $"\n*****Status: {currentTestOutcome.ToString().ToUpperInvariant()}*****" +
                      "\n____________________________________________________________________\n";

        if (currentTestOutcome != UnitTestOutcome.Passed)
        {
            AtataContext.Current?.Log.Error(message);
            try
            {
                var screenShot = AtataContext.Current?.Driver.GetScreenshot();
                var screenShotPath = TestContext.TestRunResultsDirectory + "//" + testName + "_" +
                                     DateTime.Now.ToString("HH_mm_ss_ffff") + ".png";
                screenShot?.SaveAsFile(screenShotPath, ScreenshotImageFormat.Png);
                TestContext.AddResultFile(screenShotPath);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                AtataContext.Current?.Log.Error(null, e);
            }
            finally
            {
                AtataContext.Current?.CleanUp();
            }
        }
        else
        {
            AtataContext.Current?.Log.Info(message);
        }

        ExtentContext.Reports.Flush();
        AtataContext.Current?.CleanUp();
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        AtataContext.Current?.CleanUp();
        AtataContext.Current?.Dispose();
        CloseBrowsers();
    }

    private static void CloseBrowsers()
    {
        Kill(DriverProcessName);

        if (!ClientsConfiguration.LocalMachines.Any(lm => lm.Equals(Environment.MachineName))) // Made to prevent closing all browsers after test run on local machine
        {
            Kill(BrowserProcessName);
        }
    }

    private static string GetDriverProcessName()
    {
        return AtataContext.Current.DriverAlias switch
        {
            "edge" => "MicrosoftWebDriver.exe",
            "chrome" => "ChromeDriver.exe",
            "firefox" => "GeckoDriver.exe",
            _ => throw new InvalidOperationException($"{AtataContext.Current.DriverAlias} is not supported")
        };
    }

    private static string GetBrowserProcessName()
    {
        return AtataContext.Current.DriverAlias switch
        {
            "edge" => "MicrosoftEdge.exe",
            "chrome" => "chrome.exe",
            "firefox" => "firefox.exe",
            _ => throw new InvalidOperationException($"{AtataContext.Current.DriverAlias} is not supported")
        };
    }

    private static void Kill(string processName)
    {
        try
        {
            var killCommand = $"/C taskkill /IM \"{processName}\" /T /F";

            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = killCommand
            };

            using var process = new Process { StartInfo = startInfo };
            process.Start();
            process.WaitForExit();
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
        {
            Trace.TraceWarning($"Failed to kill {processName} : {ex.Message}");
        }
    }
}