using API.Models.Dashboard;

namespace API.Services.AdminDashboard;

public interface IAdminDashboardService
{
    DashboardStatisticsApiModel Get();
}