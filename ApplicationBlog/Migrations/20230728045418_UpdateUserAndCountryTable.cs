using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationBlog.Migrations
{
    public partial class UpdateUserAndCountryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "tblUsersMaster");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "tblUsersMaster",
                newName: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "tblUsersMaster",
                newName: "StateId");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "tblUsersMaster",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
