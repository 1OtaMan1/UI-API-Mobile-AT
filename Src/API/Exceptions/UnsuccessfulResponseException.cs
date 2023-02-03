using System.Runtime.Serialization;

namespace API.Exceptions;

[Serializable]
public class UnsuccessfulResponseException : Exception
{
    public UnsuccessfulResponseException()
    {
    }

    public UnsuccessfulResponseException(string message)
        : base(message)
    {
    }

    public UnsuccessfulResponseException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected UnsuccessfulResponseException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}