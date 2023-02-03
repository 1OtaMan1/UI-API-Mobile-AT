namespace Core.Utils;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class TestCaseIdAttribute : Attribute
{
    public int Id { get; private set; }

    public TestCaseIdAttribute(int id)
    {
        Id = id;
    }
}