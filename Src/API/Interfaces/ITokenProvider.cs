using Core.EnvironmentSettings;

namespace API.Interfaces;

public interface ITokenProvider
{
    string GetToken(CredentialsStorage credentials);
}