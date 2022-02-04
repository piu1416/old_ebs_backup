using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityBillingSystem.Repositories
{
    public interface IBillRepository
    {
        Feedback CreateBill(CreateBillDTO createBillDTO);
        List<Bill> GetAllBillsAdmin();
        List<Bill> GetAllBillsCustomer(long customerId);
        Feedback UpdatePaymentStatus(UpdatePaymentDTO updatePaymentDTO);
        BillDTO GetBillById(long billId);
    }
}
