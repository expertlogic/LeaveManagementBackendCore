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
    public class LeaveTypeRepository : BaseRepository<LeaveTypes>, ILeaveTypeRepository
    {

        public LeaveTypeRepository(ApplicationDbContext db) : base(db) { }

        public async Task<ICollection<LeaveTypes>> FindAllUsingStoreProc()
        {
            var leaveTypes = await  _context.LeaveTypes.FromSqlRaw("GetLeaveTypes").ToListAsync();
            return leaveTypes;
        }
         

       
    }
}
