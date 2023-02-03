using API.Interfaces;
using API.Models.Dashboard;
using Core.Extensions;
using RestSharp;

namespace API.Services.AdminDashboard;

public class AdminDashboardService : IAdminDashboardService
{
    private const string Path = "/api/AdminDashboard";

    private readonly IRequestManager _requestManager;

    public AdminDashboardService(IRequestManager requestManager)
    {
#pragma warning disable CA2201 // Do not raise reserved exception types
        _requestManager = requestManager ?? throw new NullReferenceException(nameof(requestManager));
#pragma warning restore CA2201 // Do not raise reserved exception types
    }

    public DashboardStatisticsApiModel Get()
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path);

        return response.As<DashboardStatisticsApiModel>();
    }
}