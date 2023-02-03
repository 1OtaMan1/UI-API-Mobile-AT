using Atata;

namespace UI.Business.BaseApp.UserContribution;

using _ = SuccessInfoPage;

[PageObjectDefinition("app-information-page")]
public class SuccessInfoPage : MainPage<_>
{
    [FindByClass("title")]
    public Text<_> Title { get; private set; }

    [FindByClass("description")]
    public Text<_> Description { get; private set; }
}