using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberSystem.Domain.Entities
{
    public class EmailVerification
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public DateTime? ExpiryTime { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }

    }
}
