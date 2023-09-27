using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationBlog.Migrations
{
    public partial class Navigation_OneToMany_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAppModule_tblEmployee_ObjEmployeeEmployeeId",
                table: "tblAppModule");

            migrationBuilder.DropForeignKey(
                name: "FK_tblDepartment_tblEmployee_EmployeeId",
                table: "tblDepartment");

            migrationBuilder.DropIndex(
                name: "IX_tblDepartment_EmployeeId",
                table: "tblDepartment");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "tblDepartment");

            migrationBuilder.RenameColumn(
                name: "ObjEmployeeEmployeeId",
                table: "tblAppModule",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_tblAppModule_ObjEmployeeEmployeeId",
                table: "tblAppModule",
                newName: "IX_tblAppModule_EmployeeId");

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "tblEmployee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployee_DepartmentId",
                table: "tblEmployee",
                column: "DepartmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblAppModule_tblEmployee_EmployeeId",
                table: "tblAppModule",
                column: "EmployeeId",
                principalTable: "tblEmployee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblEmployee_tblDepartment_DepartmentId",
                table: "tblEmployee",
                column: "DepartmentId",
                principalTable: "tblDepartment",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAppModule_tblEmployee_EmployeeId",
                table: "tblAppModule");

            migrationBuilder.DropForeignKey(
                name: "FK_tblEmployee_tblDepartment_DepartmentId",
                table: "tblEmployee");

            migrationBuilder.DropIndex(
                name: "IX_tblEmployee_DepartmentId",
                table: "tblEmployee");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "tblEmployee");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "tblAppModule",
                newName: "ObjEmployeeEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_tblAppModule_EmployeeId",
                table: "tblAppModule",
                newName: "IX_tblAppModule_ObjEmployeeEmployeeId");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "tblDepartment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tblDepartment_EmployeeId",
                table: "tblDepartment",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblAppModule_tblEmployee_ObjEmployeeEmployeeId",
                table: "tblAppModule",
                column: "ObjEmployeeEmployeeId",
                principalTable: "tblEmployee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblDepartment_tblEmployee_EmployeeId",
                table: "tblDepartment",
                column: "EmployeeId",
                principalTable: "tblEmployee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
