using LeaveManagmentSystemAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Contracts
{
    public interface IUserProfileRepository : IRepositoryBase<Employee>
    {
        Task<Employee> GetByUserIdAsync(string userId);
    }
}
