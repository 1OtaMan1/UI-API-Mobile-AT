namespace Core.Utils;

[AttributeUsage(AttributeTargets.Method)]
public class StoryIdAttribute : Attribute
{
    public int Id { get; private set; }

    public StoryIdAttribute(int id)
    {
        Id = id;
    }
}