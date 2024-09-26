using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UberSystem.Domain.Entities;
using UberSystem.Domain.Enums;
using UberSystem.Domain.Interfaces.Services;
using UberSystem.Infrastructure;
using UberSytem.Dto;
using UberSytem.Dto.Requests;

namespace UberSystem.Api.EmailVerificationv1.Controllers
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
        public async Task<IActionResult> SendEmail(string Email)
        {
            if (!ModelState.IsValid) return BadRequest();
            var check = await  _verifyEmailService.SendVerificationEmailAsync(Email);
            if(check is true)
            {
                return Ok(new ApiResponseModel<string>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Success",
                });
            }
            else
            {
                return BadRequest();
            }

           
        }
    
        /// <summary>
        /// Retrieve customers in system 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(string Email,string code )
        {
            if (!ModelState.IsValid) return BadRequest();
            // Authenticate for role

             var check = await _verifyEmailService.VerifyEmail( Email,  code);
            if(check is true)
            {
                return Ok(new ApiResponseModel<string>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Success",
                });
            }
            else
            {
                return BadRequest();
            }
           
            
            
            
        }
    }
}
