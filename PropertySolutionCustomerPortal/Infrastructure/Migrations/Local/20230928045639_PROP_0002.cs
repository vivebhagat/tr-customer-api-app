using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionCustomerPortal.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientId",
                schema: "data",
                table: "Property",
                newName: "RemoteId");

            migrationBuilder.AddColumn<int>(
                name: "RemoteId",
                schema: "data",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RemoteId",
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
                name: "RemoteId",
                schema: "data",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "RemoteId",
                schema: "data",
                table: "ContractRequests");

            migrationBuilder.RenameColumn(
                name: "RemoteId",
                schema: "data",
                table: "Property",
                newName: "ClientId");
        }
    }
}
