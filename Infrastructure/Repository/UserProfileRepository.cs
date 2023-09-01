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
    public class UserProfileRepository : BaseRepository<Employee>,   IUserProfileRepository
    {

        public UserProfileRepository(ApplicationDbContext db) : base(db) { }

        public async Task<Employee> GetByUserIdAsync(string userId)
        {
            var user = await  GetByIdAsync(userId);
            return user; 
        }
    }
}
