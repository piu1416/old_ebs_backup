using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityBillingSystem.DTO
{
    public class BillDTO
    {
        public long BillId { get; set; }
        public double Amount { get; set; }
        public double Units { get; set; }
        public DateTime DuePaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerType { get; set; }
        public long ElectricityBoardId { get; set; }  //Unique BoardID
    }
}
