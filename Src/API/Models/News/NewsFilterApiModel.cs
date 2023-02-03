using API.Models.Core;

namespace API.Models.News;

public class NewsFilterApiModel : OrderedPagingApiModel
{
    public string SearchQuery { get; set; }

    public override string ToString()
    {
        return $";{nameof(FirstItemId)}:{FirstItemId}" +
               $",{nameof(Skip)}:{Skip}" +
               $",{nameof(Take)}:{Take}";
    }
}