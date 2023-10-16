using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionCustomerPortal.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractStatus",
                schema: "data",
                table: "Contracts",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "data",
                table: "ContractRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "data",
                table: "ContractRequests");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "data",
                table: "Contracts",
                newName: "ContractStatus");
        }
    }
}
