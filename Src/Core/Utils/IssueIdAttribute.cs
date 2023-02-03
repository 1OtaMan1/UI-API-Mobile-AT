namespace Core.Utils;

public class IssueIdAttribute : Attribute
{
    public int Id { get; private set; }

    public IssueIdAttribute(int id)
    {
        Id = id;
    }
}