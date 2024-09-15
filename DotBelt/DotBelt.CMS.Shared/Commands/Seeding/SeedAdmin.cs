using DotBelt.CMS.Shared.Users;

namespace DotBelt.CMS.Shared.Commands.Seeding;

public static class SeedAdmin
{
    public static ApplicationUser GetAdminUser()
    {
        var user = new ApplicationUser()
        {
            UserName = "admin@localhost",
            NormalizedUserName = "ADMIN@LOCALHOST",
            Email = "admin@localhost",
            NormalizedEmail = "ADMIN@LOCALHOST",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEOw2yqwAsSSKkNBFv7Vjt3ITxStYjh1TE//vCvc4bF91xCJ8lh0IAirb2kYOxFIGgA==",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = true,
        };

        return user;
    }
}