using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagmentSystemAPI.Data
{
    public class LogActivity
    {
        [Key]
        public int Id { get; set; }
        public string UserInfo { get; set; }
        public string Activity { get; set; }
        public string Route { get; set; }
        public string Exception { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
