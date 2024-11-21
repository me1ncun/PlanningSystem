using DAL.Enums;

namespace BLL.Security.Identity;

public class User
{
    public int UserId { get; init; }
    public List<Role> Roles { get; init; }

    public User(int userId, List<Role> roles)
    {
        UserId = userId;
        Roles = roles;
    }
}