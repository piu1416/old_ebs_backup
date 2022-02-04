using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityBillingSystem.Repositories
{
    public interface ICustomerRepository
    {
        Feedback Register(CustomerRegDTO customerRegDTO);
        CustomerDTO ViewCustomerById(long customerId);
        Feedback ChangePassword(ChangePasswordDTO changePasswordDTO);
        Feedback ForgetPassword(ForgetPasswordDTO forgetPasswordDTO);
        Customer ValidateCustomer(LoginModel login);
        Feedback UpdateCustomerProfile(UpdateProfileDTO updateProfileDTO);
        string GetTokenForCustomer(Customer customer);
    }
}
