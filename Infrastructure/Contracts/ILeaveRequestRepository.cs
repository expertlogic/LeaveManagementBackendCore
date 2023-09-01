using LeaveManagmentSystemAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Contracts
{
    public interface ILeaveRequestRepository : IRepositoryBase<LeaveRequest>
    {
        Task<ICollection<LeaveRequest>> GetLeaveRequestsByEmployee(string employeeid);
         bool CreateByProc(LeaveRequest entity);
    }
}
