using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberSystem.Domain.Entities;
using UberSystem.Domain.Enums;
using UberSystem.Domain.Interfaces;
using UberSystem.Domain.Interfaces.Services;
using UberSytem.Dto.Requests;

namespace UberSystem.Service
{
    public class VerifyEmailService : IVerifyEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VerifyEmailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public VerifyEmailService()
        {
        }

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
            var verificationCode1 = GenerateVerificationCode();
            if (verificationCode != null)
            {
                var emailveryfy = await FindUserIdByEmail(toEmail,verificationCode1);
                await _unitOfWork.Repository<EmailVerification>().UpdateAsync(emailveryfy);

                var body = $"Your verification code is: {verificationCode1}";

                await emailService.SendEmailAsync(toEmail, subject, body);
            }
           
        }
        public async Task<EmailVerification> FindUserIdByEmail(string email,string code)
        {
            try
            {
                var ListUser = await _unitOfWork.Repository<User>().GetAllAsync();
                if (ListUser != null)
                {
                    var User = ListUser.FirstOrDefault(x => x.Email == email);
                    if (User != null)
                    {
                        var a = await _unitOfWork.Repository<EmailVerification>().FindAsync(User.Id);
                        a.ExpiryTime = DateTime.UtcNow;
                        a.Code = code;
                        await _unitOfWork.Repository<EmailVerification>().UpdateAsync(a);
                        return a;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
           
                   
        }
        public async Task<User> FindEmailVerifiByEmail(string email)
        {
            return await _unitOfWork.Repository<User>().FindAsync(email);
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("VerifyEmailSender", "dohongthang258@gmail.com"));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("dohongthang258@gmail.com", "tjhr bvgd imvz slds");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
       /* public Task VerifyEmail(EmailVerifyModel model)
        {
            return null;
        }*/
    }
}
