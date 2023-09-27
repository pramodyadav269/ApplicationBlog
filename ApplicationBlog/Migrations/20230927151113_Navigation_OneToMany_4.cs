using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationBlog.Migrations
{
    public partial class Navigation_OneToMany_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SkillSetSkillId",
                table: "tblEmployee",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblSkillSet",
                columns: table => new
                {
                    SkillId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSkillSet", x => x.SkillId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployee_SkillSetSkillId",
                table: "tblEmployee",
                column: "SkillSetSkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblEmployee_tblSkillSet_SkillSetSkillId",
                table: "tblEmployee",
                column: "SkillSetSkillId",
                principalTable: "tblSkillSet",
                principalColumn: "SkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblEmployee_tblSkillSet_SkillSetSkillId",
                table: "tblEmployee");

            migrationBuilder.DropTable(
                name: "tblSkillSet");

            migrationBuilder.DropIndex(
                name: "IX_tblEmployee_SkillSetSkillId",
                table: "tblEmployee");

            migrationBuilder.DropColumn(
                name: "SkillSetSkillId",
                table: "tblEmployee");
        }
    }
}
