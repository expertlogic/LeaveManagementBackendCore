using BC170404491.FYP.Data;
using Infrastructure.Contracts;
using LeaveManagmentSystemAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class LeaveAllocationRepository : BaseRepository<LeaveAllocation>, ILeaveAllocationRepository
    {

        public LeaveAllocationRepository(ApplicationDbContext db) : base(db) { }

        public bool CheckAllocation(int leavetypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            return _context.LeaveAllocations
                .Where(q => q.EmployeeId == employeeid && q.LeaveTypeId == leavetypeid && q.Period == period)
                .Any();
        }

       

        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string employeeid)
        {
            var period = DateTime.Now.Year;
            return _context.LeaveAllocations
                .Where(q => q.EmployeeId == employeeid && q.Period == period)
                .ToList();
        }

        public LeaveAllocation GetLeaveAllocationsByEmployeeAndType(string employeeid, int leavetypeid)
        {
            var period = DateTime.Now.Year;
            return _context.LeaveAllocations
                .FirstOrDefault(q => q.EmployeeId == employeeid && q.Period == period && q.LeaveTypeId == leavetypeid);
        }
       
    }
}
