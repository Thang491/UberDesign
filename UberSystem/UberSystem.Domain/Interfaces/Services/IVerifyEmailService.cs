using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberSystem.Domain.Entities;
using UberSytem.Dto.Requests;

namespace UberSystem.Domain.Interfaces.Services
{
    public interface IVerifyEmailService
    {
        Task SendVerificationEmailAsync(string email, string VerifyCode);
       // Task VerifyEmail(EmailVerifyModel model);
    }
}
