using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationBlog.Migrations
{
    public partial class ProfileChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicPath",
                table: "tblUsersMaster");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredOn",
                table: "tblUsersMaster",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblUserProfilePic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ProfilePic = table.Column<string>(type: "varchar(max)", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserProfilePic", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUserProfilePic");

            migrationBuilder.DropColumn(
                name: "RegisteredOn",
                table: "tblUsersMaster");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicPath",
                table: "tblUsersMaster",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");
        }
    }
}
