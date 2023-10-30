using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertySolutionCustomerPortal.Infrastructure.Migrations.Local
{
    /// <inheritdoc />
    public partial class PROP_0012 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "data",
                table: "Property");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                schema: "data",
                table: "Property",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UnitType",
                schema: "data",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                schema: "data",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "UnitType",
                schema: "data",
                table: "Property");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "data",
                table: "Property",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
