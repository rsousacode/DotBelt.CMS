namespace DotBelt.CMS.Shared.Users;

public class UserResponse
{

    public int Id { get; set; } = default!;

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PhoneNumber { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }
}