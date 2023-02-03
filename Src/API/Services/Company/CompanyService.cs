using API.Interfaces;
using RestSharp;

namespace API.Services.Company;

public class CompanyService : ICompanyService
{
    private const string Path = "/api/Company";

    private readonly IRequestManager _requestManager;

    public CompanyService(IRequestManager requestManager)
    {
#pragma warning disable CA2201 // Do not raise reserved exception types
        _requestManager = requestManager ?? throw new NullReferenceException(nameof(requestManager));
#pragma warning restore CA2201 // Do not raise reserved exception types
    }

    public void Confirm(Guid companyId)
    {
        _requestManager.SendRequest(
            Method.PUT,
            Path + $"/confirm/{companyId}");
    }
}