using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationBlog.Migrations
{
    public partial class Navigation_OneToMany_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblEmployee_tblSkillSet_SkillSetSkillId",
                table: "tblEmployee");

            migrationBuilder.DropIndex(
                name: "IX_tblEmployee_SkillSetSkillId",
                table: "tblEmployee");

            migrationBuilder.DropColumn(
                name: "SkillSetSkillId",
                table: "tblEmployee");

            migrationBuilder.RenameColumn(
                name: "SkillName",
                table: "tblSkillSet",
                newName: "AppModuleName");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "tblSkillSet",
                newName: "ObjEmployeeEmployeeId");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "tblSkillSet",
                newName: "AppModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSkillSet_ObjEmployeeEmployeeId",
                table: "tblSkillSet",
                column: "ObjEmployeeEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSkillSet_tblEmployee_ObjEmployeeEmployeeId",
                table: "tblSkillSet",
                column: "ObjEmployeeEmployeeId",
                principalTable: "tblEmployee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSkillSet_tblEmployee_ObjEmployeeEmployeeId",
                table: "tblSkillSet");

            migrationBuilder.DropIndex(
                name: "IX_tblSkillSet_ObjEmployeeEmployeeId",
                table: "tblSkillSet");

            migrationBuilder.RenameColumn(
                name: "ObjEmployeeEmployeeId",
                table: "tblSkillSet",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "AppModuleName",
                table: "tblSkillSet",
                newName: "SkillName");

            migrationBuilder.RenameColumn(
                name: "AppModuleId",
                table: "tblSkillSet",
                newName: "SkillId");

            migrationBuilder.AddColumn<long>(
                name: "SkillSetSkillId",
                table: "tblEmployee",
                type: "bigint",
                nullable: true);

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
    }
}
