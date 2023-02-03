namespace API.Models.Dashboard;

public class DashboardStatisticsApiModel
{
    public int TotalCompanies { get; set; }

    public int UnverifiedCompanies { get; set; }

    public int StillWorkingCompanies { get; set; }

    public int TotalNews { get; set; }

    public int UnverifiedNews { get; set; }

    public int TotalFeedback { get; set; }

    public int UnansweredFeedback { get; set; }

    public int EmployeePetitions { get; set; }

    public int PetitionAll { get; set; }

    public int SignedBoth { get; set; }

    public List<List<DataItemApiModel>> Series { get; set; } = new();
}