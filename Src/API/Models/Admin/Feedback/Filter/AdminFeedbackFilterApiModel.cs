using API.Models.Core;
using API.Models.Enums.Admin.Feedback.Filter;
using API.Models.Enums.Core;

namespace API.Models.Admin.Feedback.Filter;

public class AdminFeedbackFilterApiModel : PagingApiModel
{
    public string SearchQuery { get; set; }

    public FeedbackListSorting Sorting { get; set; }

    public SortingDirection SortingDirection { get; set; }

    public BooleanFilter IsReplied { get; set; }
}