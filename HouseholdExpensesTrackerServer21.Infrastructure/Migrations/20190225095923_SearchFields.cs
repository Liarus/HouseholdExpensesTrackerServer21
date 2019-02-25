using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Migrations
{
    public partial class SearchFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchValue",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchValue",
                table: "SavingTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchValue",
                table: "Savings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchValue",
                table: "Roles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchValue",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchValue",
                table: "Households",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchValue",
                table: "ExpenseTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchValue",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchValue",
                table: "CredentialTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchValue",
                table: "Credentials",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CredentialTypes",
                keyColumn: "Id",
                keyValue: new Guid("105ef49d-42b6-4fb8-8d9e-52aaa16f42a9"),
                columns: new[] { "Version", "CreatedDate" },
                values: new object[] { 1, new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: new Guid("132a06e7-4c9e-49f9-8f94-0604f01a5c16"),
                columns: new[] { "Version", "CreatedDate" },
                values: new object[] { 1, new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fa64114f-9aaf-492a-a9aa-43022bfac171"),
                columns: new[] { "Version", "CreatedDate" },
                values: new object[] { 1, new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchValue",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SearchValue",
                table: "SavingTypes");

            migrationBuilder.DropColumn(
                name: "SearchValue",
                table: "Savings");

            migrationBuilder.DropColumn(
                name: "SearchValue",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "SearchValue",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "SearchValue",
                table: "Households");

            migrationBuilder.DropColumn(
                name: "SearchValue",
                table: "ExpenseTypes");

            migrationBuilder.DropColumn(
                name: "SearchValue",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "SearchValue",
                table: "CredentialTypes");

            migrationBuilder.DropColumn(
                name: "SearchValue",
                table: "Credentials");

            migrationBuilder.UpdateData(
                table: "CredentialTypes",
                keyColumn: "Id",
                keyValue: new Guid("105ef49d-42b6-4fb8-8d9e-52aaa16f42a9"),
                columns: new[] { "Version", "CreatedDate" },
                values: new object[] { 1, new DateTime(2018, 11, 21, 8, 10, 34, 317, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: new Guid("132a06e7-4c9e-49f9-8f94-0604f01a5c16"),
                columns: new[] { "Version", "CreatedDate" },
                values: new object[] { 1, new DateTime(2018, 11, 21, 8, 10, 34, 326, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fa64114f-9aaf-492a-a9aa-43022bfac171"),
                columns: new[] { "Version", "CreatedDate" },
                values: new object[] { 1, new DateTime(2018, 11, 21, 8, 10, 34, 316, DateTimeKind.Utc) });
        }
    }
}
