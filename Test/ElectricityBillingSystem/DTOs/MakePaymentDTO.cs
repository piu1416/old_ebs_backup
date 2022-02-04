using System;

namespace ElectricityBillingSystem.DTO
{
    public class MakePaymentDTO
    {
        public long BillId { get; set; }   //BillID
        public long CustomerId { get; set; }   //BillID
        public string PaymentMethod { get; set; }   //Mode Of Payment

    }
}
