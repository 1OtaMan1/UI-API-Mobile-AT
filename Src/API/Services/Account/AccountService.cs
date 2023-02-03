using API.Interfaces;
using API.Models.Auth;
using API.Rest;
using Core.EnvironmentSettings;
using Core.Extensions;
using RestSharp;

namespace API.Services.Account;

public class AccountService : IAccountService
{
    private const string Path = "/Account";

    private readonly IRequestManager _requestManager;

    public AccountService(IRequestManager requestManager)
    {
#pragma warning disable CA2201 // Do not raise reserved exception types
        _requestManager = requestManager ?? throw new NullReferenceException(nameof(requestManager));
#pragma warning restore CA2201 // Do not raise reserved exception types
    }

    public LoggedUserApiModel GetToken(LoginApiModel loginModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/authenticate",
            loginModel);

        return response.As<LoggedUserApiModel>();
    }

    public LoggedUserApiModel GetToken(CredentialsStorage userModel)
    {
        var model = new LoginApiModel { Username = userModel.Login, Password = userModel.Password};
        return GetToken(model);
    }

    public void Create(LoginApiModel loginModel)
    {
        var requestManager = new RequestManager(RestClientTypes.Admin, ApiRoles.Admin, ApplicationUrls.Api);

        requestManager.SendRequest(
            Method.POST,
            Path + "/add",
            loginModel);
    }
}