﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityBillingSystem.DTO
{
    public class UpdateProfileDTO
    {
        public long Id { get; set; } //Primary key
        public string Role { get; set; }   //Role of the person
        public string Email { get; set; }   //Email of Admin
        public string Name { get; set; }   //Name of the admin
        public string ContanctNo { get; set; } //Contanct number of the admin
    }
}
