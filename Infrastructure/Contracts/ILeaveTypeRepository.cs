﻿using LeaveManagmentSystemAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Contracts
{
    public interface ILeaveTypeRepository : IRepositoryBase<LeaveTypes>
    {
        Task<ICollection<LeaveTypes>> FindAllUsingStoreProc();

    }
}
