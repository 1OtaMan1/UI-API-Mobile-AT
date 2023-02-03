using System.Runtime.Serialization;

namespace API.Exceptions;

[Serializable]
public class ChallengeCreationException : Exception
{
    public ChallengeCreationException()
    {
    }

    public ChallengeCreationException(string message)
        : base(message)
    {
    }

    public ChallengeCreationException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected ChallengeCreationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}