using System.Runtime.Serialization;

namespace API.Exceptions;

[Serializable]
public class TokenClientException : Exception
{
    public TokenClientException()
    {
    }

    public TokenClientException(string message)
        : base(message)
    {
    }

    public TokenClientException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected TokenClientException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}