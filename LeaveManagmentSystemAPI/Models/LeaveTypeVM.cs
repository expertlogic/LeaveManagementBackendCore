using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagmentSystemAPI.Models
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefaultDays { get; set; }
        public DateTime DateCreated { get; set; }
    }   
}
