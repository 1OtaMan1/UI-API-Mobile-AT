namespace API.Models.Admin.News;

public class NewsCountApiModel
{
    public int AllNews { get; set; }

    public int UnverifiedNews { get; set; }

    public int UnconfirmedNews { get; set; }
}