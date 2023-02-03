using Atata;
using UI.Atata;

namespace UI.Business.BaseApp;

[WaitForPageLoad(TriggerEvents.BeforeAnyAction)]
public class MainPage<TOwner> : Page<TOwner>
    where TOwner : MainPage<TOwner>
{
    [FindByCss("snack-bar-container")]
    public Text<TOwner> InfoMessage { get; private set; }

    public Header HeaderElements { get; private set; }

    [ControlDefinition("app-header")]
    public class Header : Control<TOwner>
    {
        [WaitForPageLoad(TriggerEvents.BeforeClick)]
        [FindByCss("app-avatar div")]
        public Clickable<TOwner, TOwner> HomeLink { get; private set; }
    }
}