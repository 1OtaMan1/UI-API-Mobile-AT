using Atata;
using UI.Business.Common.Sections;

namespace UI.Business.Home;

using _ = MainPage;

public class MainPage : BasePage<_>
{
    public ControlList<VideoTile<_>, _> Videos { get; private set; }

    public VideoTile<_> GetVideo(string companyName) => Videos[el => el.Title.Content.Value.Contains(companyName)];
}