using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBillingSystem.Repositories
{

    public class AdminRepository:IAdminRepository
    {
        private EBS_DbContext _eBS_DbContext;

        public AdminRepository(EBS_DbContext context) {
            this._eBS_DbContext = context;
        }

        public Feedback AddCustomer(CustomerRegDTO customerRegDTO)
        {
            Feedback feedback = null;
            try
            {
                if (customerRegDTO.Password == customerRegDTO.ConfirmPassword)
                {
                    Customer customer = new Customer();
                    //Check if Customer already exists by matching Email & ElectricityBoardId
                    Customer customer1 = _eBS_DbContext.Customer.SingleOrDefault(s => s.ElectricityBoardId == customerRegDTO.ElectricityBoardId);
                    Customer customer2 = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerEmail == customerRegDTO.CustomerEmail);
                    if (customer1 == null)
                    {
                        if (customer2 == null)
                        {
                            //Add Customes
                            Role role = Role.CUSTOMER;
                            customer.Role = role.ToString();
                            customer.CustomerType = customerRegDTO.CustomerType;
                            customer.CustomerName = customerRegDTO.CustomerName;
                            customer.CustomerEmail = customerRegDTO.CustomerEmail;
                            customer.Password = customerRegDTO.Password;
                            customer.CustomerAddress = customerRegDTO.CustomerAddress;
                            customer.ElectricityBoardId = customerRegDTO.ElectricityBoardId;
                            customer.CustomerQuestion = customerRegDTO.CustomerQuestion;
                            customer.CustomerAnswer = customerRegDTO.CustomerAnswer;
                            customer.CustomerContanctNo = customerRegDTO.CustomerContanctNo;
                            _eBS_DbContext.Customer.Add(customer);
                            _eBS_DbContext.SaveChanges();
                            feedback = new Feedback() { Result = true, Message = "Customer Added" };
                        }
                        else { feedback = new Feedback() { Result = false, Message = "Customer with same Email ID already exists" }; }
                    }
                    else
                    {
                        feedback = new Feedback() { Result = false, Message = "Customer with same Electricity Board ID already exists" };

                    }
                }
                else { feedback = new Feedback() { Result = false, Message = "Password doesnot match with Confirm Password!" }; }
                

            }
            catch (Exception ex)
            {
                feedback = new Feedback() { Result = false, Message = ex.Message };

            }
            return feedback;
        }

        public Feedback ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            Admin admin1 = _eBS_DbContext.Admin.SingleOrDefault(s => s.AdminEmail == changePasswordDTO.Email);
            if (admin1 != null)
            {
                if (changePasswordDTO.OldPassword == admin1.Password)
                {
                    admin1.Password = changePasswordDTO.NewPassword;
                    _eBS_DbContext.Admin.Update(admin1);
                    _eBS_DbContext.SaveChanges();
                    Feedback feedback = new Feedback { Result = true, Message = "Password Changed" };
                    return feedback;
                }
                else
                {
                    Feedback feedback = new Feedback { Result = false, Message = "Incorrect old Password" };
                    return feedback;
                }
            }
            else
            {
                Feedback feedback = new Feedback { Result = false, Message = "Admin Email not registered!" };
                return feedback;
            }
        }

        public Feedback ForgetPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            Admin admin1 = _eBS_DbContext.Admin.SingleOrDefault(s => s.AdminEmail == forgetPasswordDTO.Email);
            if (admin1 != null)
            {
                if (forgetPasswordDTO.Answer == admin1.AdminAnswer)
                {
                    admin1.Password = forgetPasswordDTO.NewPassword;
                    _eBS_DbContext.Admin.Update(admin1);
                    _eBS_DbContext.SaveChanges();
                    Feedback feedback = new Feedback { Result = true, Message = "Password has been reset!" };
                    return feedback;
                }
                else
                {
                    Feedback feedback = new Feedback { Result = false, Message = "Incorrect Answer!" };
                    return feedback;
                }
            }
            else
            {
                Feedback feedback = new Feedback { Result = false, Message = "Admin Email not registered!" };
                return feedback;
            }
        }

        public Feedback UpdateProfile(UpdateProfileDTO updateProfileDTO)
        {
            Feedback feedback = null;
            try
            {
                //Check if Customer already exists by matching Email & ElectricityBoardId
                Admin admin = _eBS_DbContext.Admin.SingleOrDefault(s => s.AdminId == updateProfileDTO.Id);
                if (admin != null)
                {
                    admin.AdminName = updateProfileDTO.Name;
                    admin.AdminContanctNo = updateProfileDTO.ContanctNo;
                    admin.AdminEmail = updateProfileDTO.Email;
                    _eBS_DbContext.Admin.Update(admin);
                    _eBS_DbContext.SaveChanges();
                    feedback = new Feedback() { Result = true, Message = "Profile Updated!" };
                }
                else
                {
                    feedback = new Feedback() { Result = false, Message = "Invalid Admin ID!" };

                }

            }
            catch (Exception ex)
            {
                feedback = new Feedback() { Result = false, Message = ex.Message };

            }
            return feedback;
        }

        public Feedback UpdateProfileForCustomer(UpdateProfileDTO updateProfileDTO)
        {
            Feedback feedback = null;
            try
            {
                //Check if Customer already exists by matching Email & ElectricityBoardId
                Customer customer = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerId == updateProfileDTO.Id);
                if (customer != null)
                {
                    customer.CustomerName = updateProfileDTO.Name;
                    customer.CustomerContanctNo = updateProfileDTO.ContanctNo;
                    customer.CustomerEmail = updateProfileDTO.Email;
                    _eBS_DbContext.Customer.Update(customer);
                    _eBS_DbContext.SaveChanges();
                    feedback = new Feedback() { Result = true, Message = "Profile Updated!" };
                }
                else
                {
                    feedback = new Feedback() { Result = false, Message = "Invalid Customer ID!" };

                }

            }
            catch (Exception ex)
            {
                feedback = new Feedback() { Result = false, Message = ex.Message };

            }
            return feedback;
        }

        public Admin ValidateAdmin(LoginModel login)
        {
            return _eBS_DbContext.Admin.SingleOrDefault(u => u.AdminEmail == login.email && u.Password == login.password);
        }

        public AdminDTO ViewAdminById(long adminId)
        {

            Admin admin = _eBS_DbContext.Admin.SingleOrDefault(s => s.AdminId == adminId);
            if (admin != null)
            {
                AdminDTO adminDTO = new AdminDTO();
                adminDTO.AdminId = admin.AdminId;
                adminDTO.Role = admin.Role;
                adminDTO.AdminName = admin.AdminName;
                adminDTO.AdminEmail = admin.AdminEmail;
                adminDTO.AdminContanctNo = admin.AdminContanctNo;
                if (adminDTO != null)
                {
                    return adminDTO;
                }
                else { return null; }
            }
            else { return null; }

        }

        //Getting token for Authorization
        public string GetTokenForAdmin(Admin admin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                new Claim(ClaimTypes.Name, admin.AdminEmail.ToString()),
                new Claim(ClaimTypes.Role, admin.Role)
            };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public List<CustomerDTO> GetAllCustomers()
        { 
            List<Customer> customers = _eBS_DbContext.Customer.ToList();
            List<CustomerDTO> customersDTO = new List<CustomerDTO>();

            if (customers != null)
            {
                foreach (Customer x in customers)
                {
                    CustomerDTO customerDTO = new CustomerDTO();
                    customerDTO.CustomerId = x.CustomerId;
                    customerDTO.Role = x.Role;
                    customerDTO.ElectricityBoardId = x.ElectricityBoardId;
                    customerDTO.CustomerName = x.CustomerName;
                    customerDTO.CustomerType = x.CustomerType;
                    customerDTO.CustomerEmail = x.CustomerEmail;
                    customerDTO.CustomerAddress = x.CustomerAddress;
                    customerDTO.CustomerContanctNo = x.CustomerContanctNo;
                    customersDTO.Add(customerDTO);
                }
                return customersDTO;
            }
            else
            {
                return null;
            }
        }

        public Customer GetCustomerById(long customerId)
        {
            Customer customer = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerId == customerId);
            if (customer != null)
            {
                return customer;
            }
            else { return null;
            }
        }

        public Feedback DeleteCustomer(long customerId)
        {
            try
            {
                //Check if Customer exists or not
                Customer customer = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerId == customerId);
                if (customer != null)
                {
                    //Deleted Customer
                    _eBS_DbContext.Customer.Remove(customer);
                    _eBS_DbContext.SaveChanges();
                    var fb = new Feedback() { Result = true, Message = "Customer Removed" };
                    return fb;
                }
                else
                {
                    var fb = new Feedback() { Result = false, Message = "Customer doesn't exists" };
                    return fb;
                }
            }
            catch (Exception ex)
            {
                var fb = new Feedback() { Result = false, Message = ex.Message };
                return fb;
            }
        }

        public Feedback DeleteBill(long billId)
        {
            try
            {
                //Check if Bill exists or not
                Bill bill = _eBS_DbContext.Bill.SingleOrDefault(s => s.BillId == billId);
                if (bill != null)
                {
                    //Deleted Bill
                    _eBS_DbContext.Bill.Remove(bill);
                    _eBS_DbContext.SaveChanges();
                    var fb = new Feedback() { Result = true, Message = "Bill Removed" };
                    return fb;
                }
                else
                {
                    var fb = new Feedback() { Result = false, Message = "Bill doesn't exists" };
                    return fb;
                }
            }
            catch (Exception ex)
            {
                var fb = new Feedback() { Result = false, Message = ex.Message };
                return fb;
            }
        }

        public Feedback DeletePayment(long paymentId)
        {
            try
            {
                //Check if Payment exists or not
                Payment payment = _eBS_DbContext.Payment.SingleOrDefault(s => s.PaymentId == paymentId);
                if (payment != null)
                {
                    //Deleted Customer
                    _eBS_DbContext.Payment.Remove(payment);
                    _eBS_DbContext.SaveChanges();
                    var fb = new Feedback() { Result = true, Message = "Payment Removed" };
                    return fb;
                }
                else
                {
                    var fb = new Feedback() { Result = false, Message = "Payment doesn't exists" };
                    return fb;
                }
            }
            catch (Exception ex)
            {
                var fb = new Feedback() { Result = false, Message = ex.Message };
                return fb;
            }
        }
    }
}
