namespace API.Models.Core;

public class VirtualizationResult<T>
{
    /// <summary>
    /// The items on current page
    /// </summary>
    public IEnumerable<T> Items { get; set; }

    /// <summary>
    /// Checks whether has next page items
    /// </summary>
    public bool HasNext { get; set; }
}