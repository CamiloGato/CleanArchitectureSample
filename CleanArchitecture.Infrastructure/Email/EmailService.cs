using System.Net;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CleanArchitecture.Infrastructure.Email;

public class EmailService(
        IOptions<EmailSettings> emailSettingsOptions,
        ILogger<EmailService> logger
    ) :IEmailService
{
    public EmailSettings EmailSettings => emailSettingsOptions.Value;
    public async Task<bool> SendEmail(Application.Models.Email email)
    {
        var client = new SendGridClient(EmailSettings.ApiKey);
        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var body = email.Body;
        var from = new EmailAddress()
        {
            Email = EmailSettings.FromAddress,
            Name = EmailSettings.FromName,
        };

        var sendMessage = MailHelper.CreateSingleEmail(
            from, to, subject, 
            body, body    
        );

        var response = await client.SendEmailAsync(sendMessage);

        if (response.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.OK)
            return true;
        logger.LogError("The email couldn't be sent.");
        return false;
    }
}