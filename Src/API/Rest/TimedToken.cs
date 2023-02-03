using IdentityModel.Client;

namespace API.Rest;

public class TimedToken
{
    public TokenResponse Value { get; }

    public DateTime TimeStamp { get; }

    public TimedToken(TokenResponse token)
    {
        TimeStamp = DateTime.Now;
        Value = token;
    }
}