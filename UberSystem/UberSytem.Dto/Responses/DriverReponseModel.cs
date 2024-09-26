using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberSytem.Dto.Responses
{
    public class DriverReponseModel
    {
        public DateTime? Dob { get; set; }

        public double? LocationLatitude { get; set; }

        public double? LocationLongitude { get; set; }
    }
}
