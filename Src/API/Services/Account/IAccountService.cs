using API.Models.Auth;
using Core.EnvironmentSettings;

namespace API.Services.Account;

public interface IAccountService
{
    LoggedUserApiModel GetToken(LoginApiModel loginModel);

    LoggedUserApiModel GetToken(CredentialsStorage userModel);

    void Create(LoginApiModel loginModel);
}