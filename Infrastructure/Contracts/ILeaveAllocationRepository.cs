﻿using LeaveManagmentSystemAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        bool CheckAllocation(int leavetypeid, string employeeid);
        ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string employeeid);
        LeaveAllocation GetLeaveAllocationsByEmployeeAndType(string employeeid, int leavetypeid);
    }
}
