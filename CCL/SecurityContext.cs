using BLL.Security.Identity;

namespace BLL;

public class SecurityContext
{
    private static User _user = null!;

    public static User GetUser()
    {
        return _user;
    }

    public static void SetUser(User user)
    {
        _user = user;
    }
}