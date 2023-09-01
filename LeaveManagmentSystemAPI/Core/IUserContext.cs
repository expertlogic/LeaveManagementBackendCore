using Domain.Response;
using LeaveManagmentSystemAPI.Data;
using LeaveManagmentSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagmentSystemAPI.Core
{
    public interface IUserContext
    {
        public ApplicationUser Profile { get; }
    }
    public interface IUserContextBuilder
    {
        Task<IUserContext> BuildAsync(string userId);
    }
}
