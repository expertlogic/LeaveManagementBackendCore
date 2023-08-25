using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class initv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isTravelRequired",
                table: "LeaveRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "DateCreated", "DefaultDays", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 25, 12, 21, 0, 260, DateTimeKind.Local).AddTicks(7325), 15, "Sick Leave" },
                    { 2, new DateTime(2023, 8, 25, 12, 21, 0, 260, DateTimeKind.Local).AddTicks(7338), 22, "Annual Leave" },
                    { 3, new DateTime(2023, 8, 25, 12, 21, 0, 260, DateTimeKind.Local).AddTicks(7340), 5, "Maternity Leave" },
                    { 4, new DateTime(2023, 8, 25, 12, 21, 0, 260, DateTimeKind.Local).AddTicks(7341), 10, "Short Leave" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "isTravelRequired",
                table: "LeaveRequests");
        }
    }
}
