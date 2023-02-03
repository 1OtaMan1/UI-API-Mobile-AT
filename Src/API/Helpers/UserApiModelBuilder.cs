using API.Models.Admin.User;
using Core.Helpers;

namespace API.Helpers;

public class UserApiModelBuilder
{
    private readonly AdminUserAddApiModel _model;

    public UserApiModelBuilder()
    {
        _model = new AdminUserAddApiModel
        {
            Email = Generator.Email(),
            Name = Generator.FirstName(),
            Surname = Generator.LastName(),
            IsDisabled = false
        };
    }

    public AdminUserAddApiModel Build() => _model;

    public UserApiModelBuilder SetEmail(string email)
    {
        _model.Email = email;
        return this;
    }

    public UserApiModelBuilder SetFirstName(string firstName)
    {
        _model.Name = firstName;
        return this;
    }

    public UserApiModelBuilder SetLastName(string lastName)
    {
        _model.Surname = lastName;
        return this;
    }

    public UserApiModelBuilder SetStatus(bool isDisabled)
    {
        _model.IsDisabled = isDisabled;
        return this;
    }
}