using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionCustomerPortal.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0009 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                schema: "data",
                table: "Property",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AlterColumn<double>(
                name: "PurchasePrice",
                schema: "data",
                table: "Contracts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ProposedPurchasePrice",
                schema: "data",
                table: "ContractRequests",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerId",
                schema: "data",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "ManagerImage",
                schema: "data",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "ManagerName",
                schema: "data",
                table: "Property");

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchasePrice",
                schema: "data",
                table: "Contracts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProposedPurchasePrice",
                schema: "data",
                table: "ContractRequests",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
