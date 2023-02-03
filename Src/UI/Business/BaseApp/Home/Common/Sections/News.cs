using Atata;

namespace UI.Business.BaseApp.Home.Common.Sections;

public class NewsContainer : Control<NewsTab>
{
    [FindByCss("div.news-title")]
    public Clickable<ExternalPage, NewsTab> News { get; private set; }

    [FindByCss("div.date-container .date")]
    public Text<NewsTab> Date { get; private set; }
}