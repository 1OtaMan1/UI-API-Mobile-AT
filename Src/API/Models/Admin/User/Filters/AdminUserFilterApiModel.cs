using API.Models.Core;
using API.Models.Enums.Admin.User.Filters;
using API.Models.Enums.Core;

namespace API.Models.Admin.User.Filters;

public class AdminUserFilterApiModel : PagingApiModel
{
    public string SearchQuery { get; set; }

    public UserListSorting Sorting { get; set; }

    public SortingDirection SortingDirection { get; set; }

    public BooleanFilter IsDisabled { get; set; }
}