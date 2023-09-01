using LeaveManagmentSystemAPI.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagmentSystemAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new LeaveTypeConfiguration());

        }

        public DbSet<Employee> Employees { get; set; }
     
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveTypes> LeaveTypes { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LogActivity> LogActivities { get; set; }
        
    }
}
