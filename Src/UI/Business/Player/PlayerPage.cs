using Atata;

namespace UI.Business.Player;

using _ = PlayerPage;

[PageObjectDefinition("ytd-watch-flexy", ContainingClass = "ytd-page-manager")]
public class PlayerPage : BasePage<_>
{
    [FindById("title")]
    public Text<_> VideoTitle { get; private set; }
}