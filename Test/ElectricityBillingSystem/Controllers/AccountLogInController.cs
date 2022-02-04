using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ElectricityBillingSystem.Repositories;
using ElectricityBillingSystem.Models;
using ElectricityBillingSystem.Entities;
using Microsoft.AspNetCore.Authorization;
using ElectricityBillingSystem.DTO;

namespace ElectricityBillingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class AccountLogInController : ControllerBase
    {
        private IAdminRepository _adminRepository = null;   
        private ICustomerRepository _customerRepository = null;
        //Constructor
        public AccountLogInController(ICustomerRepository customerRepository,IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
            _customerRepository = customerRepository;
        }

        //Logging in Admin
        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody]LoginModel login)
        {
            if (login == null)
            {
                return BadRequest("Invalid client request");
            }
            LoggedUserModel model = new LoggedUserModel();
            //Validating Login credentials
            Admin admin = _adminRepository.ValidateAdmin(login);
            if (admin != null)
            {
                string token = _adminRepository.GetTokenForAdmin(admin);
                model = new LoggedUserModel() { Id = admin.AdminId, EmailID = admin.AdminEmail, Token = token, Role = admin.Role ,Message= "Welcome to EBills! You have been Successfully Logged In."};
                return Ok(model);
            }
            else
            {
                Customer customer = _customerRepository.ValidateCustomer(login);
                if (customer != null)
                {
                    string token = _customerRepository.GetTokenForCustomer(customer);
                    model = new LoggedUserModel() { Id = customer.CustomerId, EmailID = customer.CustomerEmail, Token = token, Role = customer.Role };
                    return Ok(model);
                }
                else
                {
                    Feedback feedback = new Feedback {Result=false,Message= "Invalid Credentials"};
                    return BadRequest(feedback);
                }
            }
        }

        [Route("ForgetPassword")]
        [HttpPut]
        public IActionResult ForgetPassword([FromBody] ForgetPasswordDTO forgetPassword)
        {
            if (forgetPassword.NewPassword == forgetPassword.ConfirmPassword) {
                if (forgetPassword.Role == "ADMIN")
                {
                    Feedback feedback = _adminRepository.ForgetPassword(forgetPassword);
                    if (feedback.Result == true)
                    {
                        return Ok(feedback);
                    }
                    else
                    {
                        return BadRequest(feedback);
                    }
                }
                else if (forgetPassword.Role == "CUSTOMER")
                {
                    Feedback feedback = _customerRepository.ForgetPassword(forgetPassword);
                    if (feedback.Result == true)
                    {
                        return Ok(feedback);
                    }
                    else
                    {
                        return BadRequest(feedback);
                    }
                }
                else
                {
                    return BadRequest("Invalid type of User!");
                }
            }
            else {
                return BadRequest("Password doesnot match with Confirm Password!");
            }
           
            //Validating Login credentials

        }

        [HttpPost]
        [Route("CustomerRegistration")]
        public IActionResult CustomerRegistration([FromBody] CustomerRegDTO customerRegDTO)
        {
            try
            {
                Feedback feedback = _customerRepository.Register(customerRegDTO);
                return Ok(feedback);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
