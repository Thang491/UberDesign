using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberSystem.Domain.Entities;
using UberSystem.Domain.Interfaces.Services;
using UberSystem.Infrastructure;
using UberSytem.Dto.Requests;

namespace UberSystem.Api.EmailVerification.Controllers
{
    public class EmailVerificationController : BaseApiController
    {
        private readonly UberSystemDbContext _context;
        private readonly IUserService _userService;
        private readonly IVerifyEmailService _verifyEmailService;

        public EmailVerificationController(UberSystemDbContext context, IUserService userService, IVerifyEmailService verifyEmailService)
        {
            _context = context;
            _userService = userService;
            _verifyEmailService = verifyEmailService;
        }
        [HttpPost("send_email")]
       /* public Task<IActionResult> SendEmail(string Email)
        {
           
        }*/
        /// <summary>
        /// Retrieve customers in system 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(EmailVerifyModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var emailVerification = await _context.EmailVerifications
                .FirstOrDefaultAsync(ev => ev.UserId == user.Id && ev.Code == model.VerificationCode);

            if (emailVerification == null || emailVerification.ExpiryTime < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired verification code.");
            }

            // Xác nhận email thành công
           // user.IsEmailConfirmed = true;
            await _context.SaveChangesAsync();

            return Ok("Email verified successfully.");
        }
    }
}
