using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagmentSystemAPI.Data
{
    public class LeaveRequest
    {
        LeaveRequest() {
            //this.RequestingEmployee = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("RequestingEmployeeId")]
        public virtual Employee RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        [ForeignKey("LeaveTypeId")]
        public virtual LeaveTypes LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public string RequestComments { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool? isTravelRequired { get; set; }
        public bool Cancelled { get; set; }
       
        [ForeignKey("ApprovedById")]
        public virtual Employee ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
    }
}
