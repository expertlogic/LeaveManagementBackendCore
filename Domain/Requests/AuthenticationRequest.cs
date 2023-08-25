﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    public class  AuthenticationRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        public string  Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string  Password { get; set; }
    }
}
