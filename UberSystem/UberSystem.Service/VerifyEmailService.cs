using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberSystem.Domain.Interfaces.Services;
using UberSytem.Dto.Requests;

namespace UberSystem.Service
{
    public class VerifyEmailService : IVerifyEmailService
    {
        public string GenerateVerificationCode(int length = 6)
        {
            var random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public async Task SendVerificationEmailAsync(string toEmail, string verificationCode)
        {
            var emailService = new VerifyEmailService();
            var subject = "Your Verification Code";
            var body = $"Your verification code is: {verificationCode}";
            await emailService.SendEmailAsync(toEmail, subject, body);
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("YourApp", "youremail@example.com"));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("youremail@example.com", "yourpassword");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        public Task VerifyEmail(EmailVerifyModel model)
        {
            throw new NotImplementedException();
        }
    }
}
