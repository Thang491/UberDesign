using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberSytem.Dto.Requests
{
    public class EmailVerifyModel
    {
        public string Email { get; set; } // Email của người dùng
        public string VerificationCode { get; set; } // Mã xác nhận
    }
}
