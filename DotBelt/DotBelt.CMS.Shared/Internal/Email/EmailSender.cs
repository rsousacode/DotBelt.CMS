using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DotBelt.CMS.Shared.Internal;

public class EmailSender : IEmailSender
{
    private readonly EmailOptions _options;
    private readonly ILogger<EmailSender> _logger;
    
    public EmailSender(IOptions<EmailOptions> optionsAccessor,
        ILogger<EmailSender> logger)
    {
        _options = optionsAccessor.Value;
        _logger = logger;
    }
    
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_options.FromName, _options.FromEmail));
        message.To.Add(new MailboxAddress(email, email));
        message.Subject = subject;
        
        message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

        using var client = new SmtpClient();
        
        await client.ConnectAsync(_options.SmtpServer, _options.SmtpPort, _options.SmtpUseSsl);
        await client.AuthenticateAsync(_options.UserName, _options.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
