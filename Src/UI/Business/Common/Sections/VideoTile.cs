using Atata;
using UI.Business.Home;
using UI.Business.Player;

namespace UI.Business.Common.Sections;

[ControlDefinition("ytd-rich-item-renderer", ContainingClass = "ytd-rich-grid-row")]
public class VideoTile<TOwner> : Control<TOwner>
    where TOwner : BasePage<TOwner>
{
    [FindById("video-title")]
    public Clickable<PlayerPage, MainPage> Title { get; private set; }
}