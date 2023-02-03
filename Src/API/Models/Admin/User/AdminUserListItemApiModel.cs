namespace API.Models.Admin.User;

public class AdminUserListItemApiModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public bool IsDisabled { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}