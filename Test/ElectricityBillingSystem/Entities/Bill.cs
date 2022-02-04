﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityBillingSystem.Entities
{
    [Table("bill")]
    public class Bill
    {
        [Key]
        public long BillId { get; set; }  //Primary Key
        
        public string CustomerType { get; set; }   //Type of the Customer

        public string CustomerName { get; set; }   //Name of the Customer

        public string CustomerAddress { get; set; }  //Address of the Customer

        public long ElectricityBoardId { get; set; }  //Unique BoardID
        public string monthAndYearOfBill { get; set; }  //Month and Year Of Bill

        [Required(ErrorMessage = "Please Enter the units Consumed")]
        public double Units { get; set; }  //Units Consumed by Customer In a Given Month

        public double BillAmount { get; set; }  //Bill Amount

        [Required]
        public DateTime DuePaymentDate { get; set; }

        public string PaymentMethod { get; set; }   //Mode Of Payment

        public DateTime PaymentDate { get; set; }   //Date Of Payment

        public string PaymentStatus { get; set; } //Payment Status


        [ForeignKey("CustomerId")]
        public long CustomerId { get; set; }   //CustomerID

        [ForeignKey("PaymentId")]
        public long PaymentId { get; set; }   //Payment ID

    }
}
