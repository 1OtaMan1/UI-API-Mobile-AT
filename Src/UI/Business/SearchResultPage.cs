using Atata;
using UI.Business.Common.Sections;

namespace UI.Business;

using _ = SearchResultPage;

public class SearchResultPage : BasePage<_>
{
    public ControlList<SearchResultVideoTile<_>, _> Videos { get; private set; }

    public SearchResultVideoTile<_> GetVideo(string companyName) => Videos[el => el.Title.Content.Value.Contains(companyName)];
}