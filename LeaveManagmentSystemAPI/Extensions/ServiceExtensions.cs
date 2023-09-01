using Infrastructure.Contracts;
using Infrastructure.Repository;
using LeaveManagmentSystemAPI.Core;
using LeaveManagmentSystemAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceStack.Web;

namespace LeaveManagmentSystemAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("Domain")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) { 
           services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
           services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
           services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
           services.AddScoped<IUserProfileRepository,  UserProfileRepository>();
            services.AddScoped<IUserContextBuilder, UserContextBuilder>();

            services.AddScoped(c =>
            {
                var httpContextAccessor = c.GetRequiredService<IHttpContextAccessor>();
                if (httpContextAccessor != null
                    && httpContextAccessor.HttpContext != null
                    && httpContextAccessor.HttpContext.User != null
                    && httpContextAccessor.HttpContext.User.Identity != null
                    && httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var userContextBuilder = c.GetRequiredService<IUserContextBuilder>();
                    var userManager = c.GetRequiredService<UserManager<Employee>>();
                    var userId = httpContextAccessor.HttpContext.GetClaimValue("userId");
                    return userContextBuilder.BuildAsync(userId).Result;
                }
                else
                {
                    IUserContext userContext = new UserContext(null);
                    return userContext;
                }
            });


        }

    }
}
