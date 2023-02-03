using System.Runtime.Serialization;

namespace API.Exceptions;

[Serializable]
public class ExpectedMessagesException : Exception
{
    public ExpectedMessagesException()
    {
    }

    public ExpectedMessagesException(string message)
        : base(message)
    {
    }

    public ExpectedMessagesException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected ExpectedMessagesException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}