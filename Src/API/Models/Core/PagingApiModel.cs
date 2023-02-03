namespace API.Models.Core;

public class PagingApiModel
{
    private const int DefaultSkipCount = 0;
    private const int MaxTakeCount = 1000;

    /// <summary>
    /// Gets or sets identifier for how many items to skip
    /// </summary>
    public int Skip { get; set; } = DefaultSkipCount;

    /// <summary>
    /// Gets or sets identifier for how many items to return
    /// </summary>
    public int Take { get; set; } = MaxTakeCount;
}