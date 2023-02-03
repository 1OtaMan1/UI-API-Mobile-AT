using API.Interfaces;
using RestSharp;

namespace API.Services.User;

public class UserService : IUserService
{
    private const string Path = "/api/User";

    private readonly IRequestManager _requestManager;

    public UserService(IRequestManager requestManager)
    {
#pragma warning disable CA2201 // Do not raise reserved exception types
        _requestManager = requestManager ?? throw new NullReferenceException(nameof(requestManager));
#pragma warning restore CA2201 // Do not raise reserved exception types
    }

    public void DeleteUser(Guid userId)
    {
        _requestManager.SendRequest(
            Method.DELETE,
            Path + $"/deleteuser/{userId}");
    }
}