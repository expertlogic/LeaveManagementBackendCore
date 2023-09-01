using AutoMapper;
using Domain.Response;
using Infrastructure.Contracts;
using LeaveManagmentSystemAPI.Data;
using LeaveManagmentSystemAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagmentSystemAPI.Core
{
    public class UserContext : IUserContext
    {
        public UserContext(
            ApplicationUser userProfile
            )
        {
            Profile = userProfile;
        }

        public ApplicationUser Profile { get; private set; }
    }

    public class UserContextBuilder : IUserContextBuilder
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public UserContextBuilder(
            IUserProfileRepository userProfileRepository ,
            IMapper mapper,
            UserManager<Employee>  userManager
            )
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IUserContext> BuildAsync(string userId)
        {
            try
            {
                var profile = await _userProfileRepository.GetByUserIdAsync(userId);

                IList<string> userRoles = await _userManager.GetRolesAsync(profile) ?? new List<string>();
                var user = _mapper.Map<ApplicationUser>(profile);
                user.UserRole = string.Join(',', userRoles).ToString();
                user.UserId = userId;
                return new UserContext(user);
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
