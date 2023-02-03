namespace API.Models.Admin.Company;

public class AdminCompanyListItemApiModel
{
    public Guid Id { get; set; }

    public string CompanyName { get; set; }

    public string WebSiteUrl { get; set; }

    public string LogoUrl { get; set; }

    public string CeoName { get; set; }

    public bool IsVerified { get; set; }

    public bool IsConfirmed { get; set; }

    public bool IsWorking { get; set; }

    public bool IsDefault { get; set; }

    public int SharesCount { get; set; }

    public int EmployeeVotesCount { get; set; }

    public int GeneralVotesCount { get; set; }

    public int LikesCount { get; set; }

    public Guid CreatedBy { get; set; }

    public string CreatorEmail { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}