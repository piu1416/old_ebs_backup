using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityBillingSystem.Entities
{
    [Table("customer")]
    public class Customer
    {
        [Key]
        public long CustomerId { get; set; }  //Primary Key

        public string Role { get; set; }   //Role of the User

        [Required(ErrorMessage = "Please Enter your BoardId")]
        public long ElectricityBoardId { get; set; }  //Unique BoardID

        public string CustomerType { get; set; }   //Type of the Customer

        [Required]
        public string CustomerQuestion { get; set; }   //Name of the admin

        [Required]
        public string CustomerAnswer { get; set; }   //Name of the admin

        [Required(ErrorMessage = "Please Enter your name")]
        public string CustomerName { get; set; }   //Name of the Customer

        [Required(ErrorMessage = "Please Enter an Email address")]
        [EmailAddress(ErrorMessage = "Enter a valid Email Address")]
        public string CustomerEmail { get; set; }   //EmailId of the Customer Used for LogIn purpose

        [Required(ErrorMessage = "Please create a password")]
        public string Password { get; set; } //Password of the Customer Used for LogIn purpose

        [Required(ErrorMessage = "Please Enter your Contanct Number")]
        [Phone(ErrorMessage = "Contanct no format not Correct")]
        public string CustomerContanctNo { get; set; } //Contanct number of the Customer

        [Required(ErrorMessage = "Please Enter your address")]
        public string CustomerAddress { get; set; }  //Address of the Customer

        public List<Bill> CustomerBills { get; set; }
        public List<Payment> CustomerPayments { get; set; }
    }
}
