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
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

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
        public async Task<bool> SendVerificationEmailAsync(string toEmail)
        {
            var emailService = new VerifyEmailService();
            var subject = "Your Verification Code";
            var verificationCode = GenerateVerificationCode();
            if (verificationCode != null)
            {
                var emailveryfy = await FindUserIdByEmail(toEmail,verificationCode);
                await _unitOfWork.Repository<EmailVerification>().UpdateAsync(emailveryfy);

                var body = $"Your verification code is: {verificationCode}";

                await emailService.SendEmailAsync(toEmail, subject, body);
                return true;
            }
            return false;
           
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
                        var listEmail = await _unitOfWork.Repository<EmailVerification>().GetAllAsync();
                        var a = listEmail.FirstOrDefault(x => x.UserId == User.Id);
                        a.ExpiryTime = DateTime.Now.AddMinutes(10) ;
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
        public async Task Add(EmailVerification email)
        {
            try
            {
                var emailVerifyRepository = _unitOfWork.Repository<EmailVerification>();

                var userRepository = _unitOfWork.Repository<User>();
                var customerRepository = _unitOfWork.Repository<Customer>();
                var driverRepository = _unitOfWork.Repository<Driver>();
                if (email is not null)
                {
                    await _unitOfWork.BeginTransaction();
                    // check duplicate user
                    var existedEmail = await emailVerifyRepository.GetAsync(u => u.UserId == email.UserId);
                    if (existedEmail is not null) throw new Exception("UserId already exists.");

                    await emailVerifyRepository.InsertAsync(email);


                    await _unitOfWork.CommitTransaction();
                }
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
        public async Task<bool> VerifyEmail(string Email,string code)
        {
            var ListUser = await _unitOfWork.Repository<User>().GetAllAsync();
            if (ListUser != null)
            {
                var User =  ListUser.FirstOrDefault(x => x.Email == Email);
                if (User != null)
                {
                    var listEmail = await _unitOfWork.Repository<EmailVerification>().GetAllAsync();
                    var Emailverify =  listEmail.FirstOrDefault(x => x.UserId == User.Id);
                    if(Emailverify.Code == code && Emailverify.ExpiryTime >= DateTime.Now)
                    {
                        User.IsEmailConfirmed = true;
                        await _unitOfWork.Repository<User>().UpdateAsync(User);
                        return true;
                    }
                    return false;

                }
                return false;               
            }
            return false;

        }
    }
}
