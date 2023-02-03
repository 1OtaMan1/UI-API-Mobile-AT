namespace API.Models.Admin.Company;

public class AdminCompanyAddApiModel
{
    public Guid Id { get; set; }

    public string CompanyName { get; set; }

    public string WebSiteUrl { get; set; }

    public string LogoUrl { get; set; }

    public string CeoName { get; set; }

    public bool IsWorking { get; set; }

    public bool IsVerified { get; set; }

    public bool IsConfirmed { get; set; }
}