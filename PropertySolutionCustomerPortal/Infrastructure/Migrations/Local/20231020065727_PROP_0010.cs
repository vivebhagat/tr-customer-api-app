using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionCustomerPortal.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0010 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerImage",
                schema: "data",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "ManagerName",
                schema: "data",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                schema: "data",
                table: "Property",
                newName: "PropertyManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PropertyManagerId",
                schema: "data",
                table: "Property",
                newName: "ManagerId");

            migrationBuilder.AddColumn<string>(
                name: "ManagerImage",
                schema: "data",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerName",
                schema: "data",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
