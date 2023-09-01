using BC170404491.FYP.Data;
using Infrastructure.Contracts;
using LeaveManagmentSystemAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class LeaveRequestRepository : BaseRepository<LeaveRequest>, ILeaveRequestRepository
    {
 

        public LeaveRequestRepository(ApplicationDbContext db) : base(db) { }



        public async Task<ICollection<LeaveRequest>> GetLeaveRequestsByEmployee(string employeeid)
        {
            var leaveRequests = await _context.LeaveRequests
                .Where(q => q.RequestingEmployeeId == employeeid)
                .ToListAsync();
            return leaveRequests;
        }

        public bool CreateByProc(LeaveRequest entity)
        {
            var param = new SqlParameter("@RequestingEmployeeId", entity.RequestingEmployeeId);
            var StartDate = new SqlParameter("@StartDate", entity.StartDate.ToShortDateString());
            var EndDate = new SqlParameter("@EndDate", entity.EndDate.ToShortDateString());
            var LeaveTypeId = new SqlParameter("@LeaveTypeId", entity.LeaveTypeId);
            var sTravelRequired = new SqlParameter("@IsTravelRequired", entity.isTravelRequired);
            var RequestComments = new SqlParameter("@RequestComments", entity.RequestComments);

            List<SqlParameter> parms = new List<SqlParameter> { param, StartDate, EndDate, LeaveTypeId, sTravelRequired, RequestComments };


            var rowsChanges = _context.Database.ExecuteSqlRaw("sp_LeaveRequest {0}, {1}, {2}, {3}, {4}, {5}",
                entity.RequestingEmployeeId, entity.StartDate, entity.EndDate, entity.LeaveTypeId, entity.isTravelRequired, entity.RequestComments);

            return true;


        }
    }
}
