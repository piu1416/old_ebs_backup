using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityBillingSystem.Models
{
    public class LoggedUserModel
    {
        public long Id { get; set; }
        public string EmailID { get; set; }
        public string Role { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }

    }
}
