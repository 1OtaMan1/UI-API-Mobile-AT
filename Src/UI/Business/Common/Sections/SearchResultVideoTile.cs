using Atata;
using UI.Business.Player;

namespace UI.Business.Common.Sections;

[ControlDefinition("ytd-video-renderer", ContainingClass = "ytd-item-section-renderer")]
public class SearchResultVideoTile<TOwner> : Control<TOwner>
    where TOwner : BasePage<TOwner>
{
    [FindById("video-title")]
    public Clickable<PlayerPage, SearchResultPage> Title { get; private set; }
}