

using System.Reflection;
using Domain.Enums;
using Domain.Interfaces.Communication;
using Infrastructure.Common.ConfigModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using WorkingGood.Log;

namespace Infrastructure.Communication.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IWgLog<EmailSender> _logger;
        private readonly EmailConfig _emailConfig;
        public EmailSender(IWgLog<EmailSender> logger, EmailConfig emailConfig)
        {
            _logger = logger;
            _emailConfig = emailConfig;
        }
        public async Task Send(string content, string subject, string recipient)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_emailConfig.Login));
                email.To.Add(MailboxAddress.Parse(recipient));
                email.Subject = subject;
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = content;
                email.Body = bodyBuilder.ToMessageBody();
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_emailConfig.Server, _emailConfig.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailConfig.Login, _emailConfig.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                _logger.Info("Sent email message");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        public Task Send(MessageDestinations messageDestinations, string recipients)
        {
            string path = @"EmailTemplates/ResetPassword.html";
            string htmlFile = File.ReadAllText(path);
            return Task.CompletedTask;
        }
    }
}

