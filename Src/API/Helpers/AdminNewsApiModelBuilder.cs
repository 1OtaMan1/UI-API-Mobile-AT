using API.Models.Admin.News;
using Core.Helpers;

namespace API.Helpers;

public class AdminNewsApiModelBuilder
{
    private readonly AdminNewsAddApiModel _model;

    public AdminNewsApiModelBuilder(Guid companyId)
    {
        _model = new AdminNewsAddApiModel
        {
            CompanyId = companyId,
            Title = Generator.News(),
            Url = Generator.Url(),
            Date = DateTimeOffset.Now,
            IsVerified = false,
            IsConfirmed = false
        };
    }

    public AdminNewsAddApiModel Build() => _model;

    public AdminNewsApiModelBuilder SetId(Guid id)
    {
        _model.Id = id;
        return this;
    }

    public AdminNewsApiModelBuilder SetTitle(string title)
    {
        _model.Title = title;
        return this;
    }

    public AdminNewsApiModelBuilder SetUrl(string url)
    {
        _model.Url = url;
        return this;
    }

    public AdminNewsApiModelBuilder SetCreationDate(DateTimeOffset date)
    {
        _model.Date = date;
        return this;
    }

    public AdminNewsApiModelBuilder SetVerificationStatus(bool isVerified)
    {
        _model.IsVerified = isVerified;
        return this;
    }

    public AdminNewsApiModelBuilder SetConfirmationStatus(bool isConfirmed)
    {
        _model.IsConfirmed = isConfirmed;
        return this;
    }
}