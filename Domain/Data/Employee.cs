using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LeaveManagmentSystemAPI.Data
{
    public class Employee : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string TaxId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }

    }
}
