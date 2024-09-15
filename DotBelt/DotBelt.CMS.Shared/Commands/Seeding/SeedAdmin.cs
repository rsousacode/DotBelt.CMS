using DotBelt.CMS.Shared.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DotBelt.CMS.Shared.Commands.Seeding;

public static class SeedAdmin
{
    public static async Task<ApplicationUser> GetCreatedAdminUser(IServiceProvider sp)
    {
        var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();

        if (!userManager.SupportsUserEmail)
        {
            throw new NotSupportedException($"SeedAdmin requires a user store with email support.");
        }

        var userStore = sp.GetRequiredService<IUserStore<ApplicationUser>>();
        var emailStore = (IUserEmailStore<ApplicationUser>)userStore;
        var email = "admin@localhost";

        var user = new ApplicationUser();
        user.EmailConfirmed = true;
        await userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);
        await userManager.CreateAsync(user, "Admin123$");

        return user;

    }
}