using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationBlog.Migrations
{
    public partial class Navigation_OneToMany_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSkillSet_tblEmployee_ObjEmployeeEmployeeId",
                table: "tblSkillSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblSkillSet",
                table: "tblSkillSet");

            migrationBuilder.RenameTable(
                name: "tblSkillSet",
                newName: "tblAppModule");

            migrationBuilder.RenameIndex(
                name: "IX_tblSkillSet_ObjEmployeeEmployeeId",
                table: "tblAppModule",
                newName: "IX_tblAppModule_ObjEmployeeEmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblAppModule",
                table: "tblAppModule",
                column: "AppModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblAppModule_tblEmployee_ObjEmployeeEmployeeId",
                table: "tblAppModule",
                column: "ObjEmployeeEmployeeId",
                principalTable: "tblEmployee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAppModule_tblEmployee_ObjEmployeeEmployeeId",
                table: "tblAppModule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblAppModule",
                table: "tblAppModule");

            migrationBuilder.RenameTable(
                name: "tblAppModule",
                newName: "tblSkillSet");

            migrationBuilder.RenameIndex(
                name: "IX_tblAppModule_ObjEmployeeEmployeeId",
                table: "tblSkillSet",
                newName: "IX_tblSkillSet_ObjEmployeeEmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblSkillSet",
                table: "tblSkillSet",
                column: "AppModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSkillSet_tblEmployee_ObjEmployeeEmployeeId",
                table: "tblSkillSet",
                column: "ObjEmployeeEmployeeId",
                principalTable: "tblEmployee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
