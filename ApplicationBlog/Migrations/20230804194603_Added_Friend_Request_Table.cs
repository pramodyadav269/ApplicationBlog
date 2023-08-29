using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationBlog.Migrations
{
    public partial class Added_Friend_Request_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblFriendRequest",
                columns: table => new
                {
                    RequestId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedBy = table.Column<long>(type: "bigint", nullable: true),
                    RequestedTo = table.Column<long>(type: "bigint", nullable: true),
                    IsRequestAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsRequestRejected = table.Column<bool>(type: "bit", nullable: false),
                    IsUnfriend = table.Column<bool>(type: "bit", nullable: false),
                    RequestedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AcceptedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UnfriendOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFriendRequest", x => x.RequestId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFriendRequest");
        }
    }
}
