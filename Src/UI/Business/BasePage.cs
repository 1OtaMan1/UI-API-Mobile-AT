using Atata;
using UI.Atata;

namespace UI.Business;

[WaitForPageLoad(TriggerEvents.BeforeAnyAction)]
public class BasePage<TOwner> : Page<TOwner>
    where TOwner : BasePage<TOwner>
{
    public Header HeaderElements { get; private set; }

    [ControlDefinition("div[@id='masthead-container']")]
    public class Header : Control<TOwner>
    {
        [FindById("search")]
        public TextInput<TOwner> SearchInput { get; private set; }

        [WaitForPageLoad(TriggerEvents.BeforeClick)]
        [FindById("search-icon-legacy")]
        public Button<SearchResultPage, TOwner> SearchIcon { get; private set; }
    }
}