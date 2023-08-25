using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class spLeaveRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 8, 25, 17, 53, 17, 383, DateTimeKind.Local).AddTicks(9642));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 8, 25, 17, 53, 17, 383, DateTimeKind.Local).AddTicks(9659));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2023, 8, 25, 17, 53, 17, 383, DateTimeKind.Local).AddTicks(9660));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2023, 8, 25, 17, 53, 17, 383, DateTimeKind.Local).AddTicks(9661));

            var sp = @"CREATE PROCEDURE[dbo].[GetLeaveTypes]
                            AS
                        BEGIN
                            SET NOCOUNT ON;
                            select Id, [Name], DefaultDays, DateCreated  from LeaveTypes
                        END ";

            migrationBuilder.Sql(sp);

            var sp2 = @"Create PROCEDURE [dbo].[sp_LeaveAllocations]
                          @MODE int,
	                      @NumberOfDays int ,
	                      @DateCreated date,
	                      @EmployeeId nvarchar(450),
	                      @LeaveTypeId int ,
	                      @Period int
	                      AS
                            BEGIN
                                SET NOCOUNT ON;
			                    if @MODE = 1 
			                    begin
			                       SELECT   [Id]
					                      ,[NumberOfDays]
					                      ,[DateCreated]
					                      ,[EmployeeId]
					                      ,[LeaveTypeId]
					                      ,[Period]
				                      FROM [LeaveManagent].[dbo].[LeaveAllocations]
			                    end
			                    if @MODE = 2 
			                    begin
			                      INSERT INTO [dbo].[LeaveAllocations]
					                       ([NumberOfDays]
					                       ,[DateCreated]
					                       ,[EmployeeId]
					                       ,[LeaveTypeId]
					                       ,[Period])
				                     VALUES
					                       (@NumberOfDays  
					                       ,@DateCreated  
					                       ,@EmployeeId  
					                       ,@LeaveTypeId  
					                       ,@Period )
		 
		                       end

                            END";

            migrationBuilder.Sql(sp2);

            var spInsertLeave = @"CREATE PROCEDURE sp_LeaveRequest
	                                @RequestingEmployeeId nvarchar(450),
	                                @StartDate datetime ,
	                                @EndDate  datetime ,
	                                @LeaveTypeId   int,
	                                @isTravelRequired  bit,
	                                @RequestComments nvarchar(max)
                                AS
                                BEGIN
	                                SET NOCOUNT ON;

	                                 INSERT INTO [dbo].[LeaveRequests]
                                           ([RequestingEmployeeId]
                                           ,[StartDate]
                                           ,[EndDate]
                                           ,[LeaveTypeId]
                                           ,[DateRequested]
                                           ,[RequestComments]
                                           ,[Approved]
										   ,Cancelled
										   ,DateActioned
                                           ,[isTravelRequired])
                                     VALUES
                                           (@RequestingEmployeeId
                                           ,@StartDate
                                           ,@EndDate
                                           ,@LeaveTypeId
                                           ,GetDate()
                                           ,@RequestComments
                                           ,null
										   ,0
										   ,GetDate()
                                           ,@isTravelRequired)

                                END";

            migrationBuilder.Sql(spInsertLeave);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 8, 25, 16, 6, 29, 418, DateTimeKind.Local).AddTicks(1677));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 8, 25, 16, 6, 29, 418, DateTimeKind.Local).AddTicks(1691));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2023, 8, 25, 16, 6, 29, 418, DateTimeKind.Local).AddTicks(1692));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2023, 8, 25, 16, 6, 29, 418, DateTimeKind.Local).AddTicks(1692));
        }
    }
}
