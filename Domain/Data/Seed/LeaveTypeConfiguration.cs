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
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveTypes>
    {
        public void Configure(EntityTypeBuilder<LeaveTypes> builder)
        {

            builder.HasData
            (
                 new LeaveTypes
                 {
                     Id =1,
                     DefaultDays =15,
                     Name = "Sick Leave",
                     DateCreated = DateTime.Now
                 } ,
                 new LeaveTypes
                 {
                     Id =2,
                     DefaultDays = 22,
                     Name = "Annual Leave",
                     DateCreated = DateTime.Now
                 },
                 new LeaveTypes
                 {
                     Id = 3,
                     DefaultDays = 5,
                     Name = "Maternity Leave",
                     DateCreated = DateTime.Now
                 },
                 new LeaveTypes
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
