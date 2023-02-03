using Core.Helpers;

namespace UI.Models;

public class UserUiModel
{
    public Guid Id { get; set; }

    public string Email { get; set; } = Generator.Email();

    public string FirstName { get; set; } = Generator.FirstName();

    public string LastName { get; set; } = Generator.LastName();

    public bool IsDisabled { get; set; } = false;
}