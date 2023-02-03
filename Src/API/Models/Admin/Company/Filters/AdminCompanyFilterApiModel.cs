using API.Models.Core;
using API.Models.Enums.Admin.Company.Filters;
using API.Models.Enums.Core;

namespace API.Models.Admin.Company.Filters;

public class AdminCompanyFilterApiModel : PagingApiModel
{
    public string SearchQuery { get; set; }

    public CompanyListSorting Sorting { get; set; }

    public SortingDirection SortingDirection { get; set; }

    public BooleanFilter IsVerified { get; set; }

    public BooleanFilter IsConfirmed { get; set; }

    public BooleanFilter IsWorking { get; set; }
}