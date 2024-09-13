namespace DotBelt.CMS.Shared.Internal;

public class EmailOptions
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string FromEmail { get; set; }
    public required string FromName { get; set; }
    public required string SmtpServer { get; set; }
    public required int SmtpPort { get; set; }
    public required bool SmtpUseSsl { get; set; }
}