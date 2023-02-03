namespace API.Models.Core;

public class OrderedPagingApiModel : PagingApiModel
{
    /// <summary>
    /// Gets or sets the first item identifier.
    /// </summary>
    public Guid? FirstItemId { get; set; }
}