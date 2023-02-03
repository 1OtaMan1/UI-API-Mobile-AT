using Core.Helpers;

namespace UI.Models;

public class NewsUiModel
{
    public Guid Id { get; set; }

    public string CompanyName { get; set; } = Generator.CompanyName();

    public string CompanyUrl { get; set; } = Generator.Url();

    public List<string> NewsUrl { get; set; } = new() {Generator.Url()};
}