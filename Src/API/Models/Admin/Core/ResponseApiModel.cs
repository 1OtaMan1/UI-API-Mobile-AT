namespace API.Models.Admin.Core;

public class ResponseApiModel
{
    public Guid Id { get; set; }

    public List<string> Errors { get; set; } = new();
}