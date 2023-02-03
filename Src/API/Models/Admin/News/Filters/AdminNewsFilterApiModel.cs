using API.Models.Core;
using API.Models.Enums.Admin.News.Filters;
using API.Models.Enums.Core;

namespace API.Models.Admin.News.Filters;

public class AdminNewsFilterApiModel : PagingApiModel
{
    public string SearchQuery { get; set; }

    public NewsListSorting Sorting { get; set; }

    public SortingDirection SortingDirection { get; set; }

    public BooleanFilter IsVerified { get; set; }

    public BooleanFilter IsConfirmed { get; set; }
}