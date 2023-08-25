using LeaveManagmentSystemAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagmentSystemAPI
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {

            builder.HasData
            (
                 new LeaveType
                 {
                     Id =1,
                     DefaultDays =15,
                     Name = "Sick Leave",
                     DateCreated = DateTime.Now
                 } ,
                 new LeaveType
                 {
                     Id =2,
                     DefaultDays = 22,
                     Name = "Annual Leave",
                     DateCreated = DateTime.Now
                 },
                 new LeaveType
                 {
                     Id = 3,
                     DefaultDays = 5,
                     Name = "Maternity Leave",
                     DateCreated = DateTime.Now
                 },
                 new LeaveType
                 {
                     Id = 4,
                     DefaultDays = 10,
                     Name = "Short Leave",
                     DateCreated = DateTime.Now
                 }
            );

        }

    }
}
