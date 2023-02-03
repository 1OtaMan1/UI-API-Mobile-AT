using System.Collections.ObjectModel;
using Atata;
using Core.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using static Core.Constants.Sizes;

namespace UI.Atata;

public static class AtataUtils
{
    private const string GetLengthToPageTopNotForEdge = "return document.documentElement.scrollTop;";
    private const string ScrollToTheBottomNotEdge = "window.scrollBy(0, document.body.scrollHeight);";
    private const string ScrollToTheTopNotEdge = "window.scrollBy(0, -document.body.scrollHeight);";

    public static ReadOnlyCollection<Cookie> GetCookies()
    {
        return AtataContext.Current.Driver.Manage().Cookies.AllCookies;
    }

    public static void SetCookies(IEnumerable<Cookie> cookies)
    {
        cookies
            .ToList()
            .ForEach(c => { AtataContext.Current.Driver.Manage().Cookies.AddCookie(c); });
    }

    public static bool IsEdgeDriver => AtataContext.Current.DriverAlias == "edge";

    public static string CurrentDirectory => AppDomain.CurrentDomain.BaseDirectory;

    public static void WaitForSurveyRequestComplete() => Thread.Sleep(1500);

    public static void EmulatePasteAction()
    {
        var actions = new Actions(AtataContext.Current.Driver);
        actions.KeyDown(Keys.Control);
        actions.SendKeys("v");
        actions.KeyUp(Keys.Control);
        actions.Build().Perform();
    }

    public static void EmulateSelectTextAction()
    {
        var actions = new Actions(AtataContext.Current.Driver);
        actions.KeyDown(Keys.Control);
        actions.SendKeys("a");
        actions.KeyUp(Keys.Control);
        actions.Build().Perform();
    }

    // Can be used to switch user in scope of one test without logout
    public static void RestartDriver()
    {
        AtataContext.Current.RestartDriver();
        if (IsEdgeDriver)
        {
            AtataContext.Current.Driver.Maximize();
        }
    }

    public static T OpenNewTab<T>() where T : Page<T>
    {
        AtataContext.Current.Driver.ExecuteScript("window.open()");
        return Go.ToNextWindow<T>();
    }

    public static void ScrollDown(int skipPagePart = 2)
    {
        WaitForSpinner();

        while (!IsScrolledToBottom())
        {
            AtataContext.Current.Driver.ExecuteScript($"window.scrollBy(0, document.documentElement.clientHeight/{skipPagePart});");
        }
    }

    public static bool IsScrolledToBottom()
    {
        var bottomScrollPosition = Convert.ToInt64(AtataContext.Current.Driver.ExecuteScript(GetLengthToPageTopNotForEdge));

        var currentScrollPosition = (long)AtataContext.Current.Driver.ExecuteScript("return document.documentElement.scrollHeight;") -
                                    (long)AtataContext.Current.Driver.ExecuteScript("return document.documentElement.clientHeight;");

        return bottomScrollPosition.Equals(currentScrollPosition);
    }

    public static bool IsScrolledToTop()
    {
        var position = Convert.ToInt64(AtataContext.Current.Driver.ExecuteScript(GetLengthToPageTopNotForEdge));
        return position == default;
    }

    public static void ScrollToTheBottom()
    {
        WaitForSpinner();

        while (!IsScrolledToBottom())
        {
            AtataContext.Current.Driver.ExecuteScript(ScrollToTheBottomNotEdge);
        }
    }

    public static void ScrollToTheTop()
    {
        WaitForSpinner();

        while (!IsScrolledToTop())
        {
            AtataContext.Current.Driver.ExecuteScript(ScrollToTheTopNotEdge);
        }
    }

    public static void ScrollTopBy(int pixels)
    {
        WaitForSpinner();

        while (!IsScrolledToTop())
        {
            AtataContext.Current.Driver.ExecuteScript($"window.scrollBy(0, -{pixels});");
        }
    }

    public static void WaitForSpinner(int times = 2)
    {
        var script = @"return (!window.isPendingRequest) && 
                   (document.getElementsByClassName('spin-box').length == 0) &&
                   (document.getElementsByClassName('backdrop').length == 0) &&
                   (document.getElementsByClassName('ng-pending').length == 0) &&
                   (document.getElementsByClassName('spinner').length == 0);";

        for (var i = 0; i < times; i++)
        {
            var result = false;
            var attempts = 10;

            while (!result && attempts-- > 0)
            {
                Thread.Sleep(1000);
                result = (bool)AtataContext.Current.Driver.ExecuteScript(script);
            }
        }
    }

    public static void JavascriptClick(string elementClass, int elementIndex = 0)
    {
        Retry.Exponential<WebDriverException>(RepeatActionTimes,
            () => AtataContext.Current.Driver.ExecuteScript($"document.getElementsByClassName('{elementClass}')[{elementIndex}].click();"));
    }

    public static void JavascriptFocus(string elementClass, int elementIndex = 0)
    {
        Retry.Exponential<WebDriverException>(RepeatActionTimes,
            () => AtataContext.Current.Driver.ExecuteScript($"document.getElementsByClassName('{elementClass}')[{elementIndex}].focus();"));
    }
}

public class PressKeysAttribute : TriggerAttribute
{
    public PressKeysAttribute(string keys, TriggerEvents on = TriggerEvents.AfterSet, TriggerPriority priority = TriggerPriority.Medium)
        : base(on, priority)
    {
        Keys = keys;
    }

    public string Keys { get; protected set; }

    protected override void Execute<TOwner>(TriggerContext<TOwner> context)
    {
        if (!string.IsNullOrEmpty(Keys))
        {
            context.Driver.Perform(x => x.SendKeys(Keys));
        }
    }
}