using Core.EnvironmentSettings;

namespace API.Interfaces;

public interface IUserCreator
{
#pragma warning disable S4581 // "new Guid()" should not be used
    CredentialsStorage AddUser(CredentialsStorage userCredentials = null, Guid officeId = default, bool acceptGdpr = true,
#pragma warning restore S4581 // "new Guid()" should not be used
        bool turnOffOnboardingAndChecklist = true, bool isExternal = false);
}