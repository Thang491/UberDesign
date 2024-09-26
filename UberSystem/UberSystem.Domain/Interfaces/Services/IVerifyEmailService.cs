using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberSystem.Domain.Entities;

namespace UberSystem.Domain.Interfaces.Services
{
    public interface IVerifyEmailService
    {
        Task<bool> SendVerificationEmailAsync(string email);
        Task<bool> VerifyEmail(string email , string code);
        Task Add(EmailVerification email);

    }
}
