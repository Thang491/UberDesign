using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberSytem.Dto.Responses
{
    public class CustomerRatingReponseModel
    {
        public long? CustomerId { get; set; }

        public long? DriverId { get; set; }

        public long? TripId { get; set; }
        public int? Rating1 { get; set; }

        public string? Feedback { get; set; }
    }
}
