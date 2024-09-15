using DotBelt.CMS.Shared.Users;
using Microsoft.AspNetCore.Identity;

namespace DotBelt.CMS.Shared.Identity;

public class IdentityEmailSender : IEmailSender<ApplicationUser>
{
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        return Task.CompletedTask;

    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        return Task.CompletedTask;

    }
}