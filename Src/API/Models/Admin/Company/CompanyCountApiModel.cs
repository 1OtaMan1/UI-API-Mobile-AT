namespace API.Models.Admin.Company;

public class CompanyCountApiModel
{
    public int AllCompanies { get; set; }

    public int UnverifiedCompanies { get; set; }

    public int UnconfirmedCompanies { get; set; }
}