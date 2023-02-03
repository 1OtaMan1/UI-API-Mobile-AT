namespace API.Models.Admin.User;

public class AdminUserDetailsApiModel : AdminUserListItemApiModel
{
    public bool IsAdmin { get; set; }
}