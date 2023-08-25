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
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(LeaveRequest entity)
        {
            _db.LeaveRequests.Add(entity);
            return Save();
        }

        public bool Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return Save();
        }

        public ICollection<LeaveRequest> FindAllUsingStoreProc()
        {
            var LeaveHistories = _db.LeaveRequests
                .Include(q => q.RequestingEmployee)
                .Include(q => q.ApprovedBy)
                .Include(q => q.LeaveType)
                .ToList();
            return LeaveHistories;
        }
        public ICollection<LeaveRequest> FindAll()
        {
            var LeaveHistories = _db.LeaveRequests
                .Include(q => q.RequestingEmployee)
                .Include(q => q.ApprovedBy)
                .Include(q => q.LeaveType)
                .ToList();
            return LeaveHistories;
        }

        public LeaveRequest FindById(int id)
        {
            var LeaveHistory = _db.LeaveRequests
                .Include(q => q.RequestingEmployee)
                .Include(q => q.ApprovedBy)
                .Include(q => q.LeaveType)
                .FirstOrDefault(q => q.Id == id);
            return LeaveHistory;
        }

        public ICollection<LeaveRequest> GetLeaveRequestsByEmployee(string employeeid)
        {
            var leaveRequests = FindAll()
                .Where(q => q.RequestingEmployeeId == employeeid)
                .ToList();
            return leaveRequests;
        }

        public bool isExists(int id)
        {
            var exist = _db.LeaveRequests.Any(q => q.Id == id);
            return exist;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return Save();
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


            var rowsChanges = _db.Database.ExecuteSqlRaw("sp_LeaveRequest {0}, {1}, {2}, {3}, {4}, {5}", 
                entity.RequestingEmployeeId , entity.StartDate , entity.EndDate ,entity.LeaveTypeId , entity.isTravelRequired , entity.RequestComments);
             
                return true;
            

        }
    }
}
