using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityBillingSystem.DTO
{
    public class ForgetPasswordDTO
    {
        public string Role { get; set; }
        public string Email { get; set; }
        public string Answer { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
