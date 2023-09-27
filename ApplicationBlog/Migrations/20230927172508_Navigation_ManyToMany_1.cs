using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationBlog.Migrations
{
    public partial class Navigation_ManyToMany_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblProject",
                columns: table => new
                {
                    ProjectId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProject", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "tblEmployeeProjectMapping",
                columns: table => new
                {
                    MappingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEmployeeProjectMapping", x => x.MappingId);
                    table.ForeignKey(
                        name: "FK_tblEmployeeProjectMapping_tblEmployee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tblEmployee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblEmployeeProjectMapping_tblProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "tblProject",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployeeProjectMapping_EmployeeId",
                table: "tblEmployeeProjectMapping",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployeeProjectMapping_ProjectId",
                table: "tblEmployeeProjectMapping",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblEmployeeProjectMapping");

            migrationBuilder.DropTable(
                name: "tblProject");
        }
    }
}
