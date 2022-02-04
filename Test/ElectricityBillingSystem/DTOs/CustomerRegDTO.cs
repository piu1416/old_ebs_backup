using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityBillingSystem.DTO
{
    public class CustomerRegDTO
    {
        public long ElectricityBoardId { get; set; }  //Unique BoardID
        public string CustomerType { get; set; }   //Type of the Customer
        public string CustomerName { get; set; }   //Name of the Customer
        public string CustomerEmail { get; set; }   //EmailId of the Customer Used for LogIn purpose
        public string CustomerContanctNo { get; set; } //Contanct number of the Customer
        public string CustomerAddress { get; set; }  //Address of the Customer
        public string CustomerQuestion { get; set; }
        public string CustomerAnswer { get; set; } 
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
