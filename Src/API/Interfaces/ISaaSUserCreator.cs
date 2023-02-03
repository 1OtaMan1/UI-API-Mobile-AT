using Core.EnvironmentSettings;

namespace API.Interfaces;

public interface ISaaSUserCreator
{
    CredentialsStorage AddUser(CredentialsStorage userCredentials = null, bool activateUser = true, bool acceptGdpr = true);
}