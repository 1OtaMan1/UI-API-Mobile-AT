using Core.Helpers;

namespace UI.Models;

public class CompanyUiModel
{
    public Guid Id { get; set; }

    public string CompanyName { get; set; } = Generator.CompanyName();

    public string WebSiteUrl { get; set; } = Generator.Url();

    public string LogoUrl { get; set; } = Generator.Url();

    public string CeoName { get; set; } = Generator.FirstName() + " " + Generator.LastName();
}