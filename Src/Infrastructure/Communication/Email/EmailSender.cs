

using Domain.Interfaces.Communication;
using Infrastructure.Common.ConfigModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Communication.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfig _emailConfig;
        public EmailSender(EmailConfig emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public async Task Send(string content, string subject, string recipient)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailConfig.Login));
            email.To.Add(MailboxAddress.Parse(recipient));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Text) { Text = content};
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailConfig.Server, _emailConfig.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailConfig.Login, _emailConfig.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}

