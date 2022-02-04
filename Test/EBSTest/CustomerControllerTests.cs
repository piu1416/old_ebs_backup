using ElectricityBillingSystem.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBSTest
{
    public class CustomerControllerTests
    {
        private Mock<ICustomerRepository> mockCustomerRepo;
        private Mock<IBillRepository> mockBillRepo;
        private Mock<IPaymentRepository> mockPaymentRepo;

        public CustomerControllerTests()
        {
            mockCustomerRepo = new Mock<ICustomerRepository>();
            mockBillRepo = new Mock<IBillRepository>();
            mockPaymentRepo = new Mock<IPaymentRepository>();
        }


    }
}
