using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationBlog.Migrations
{
    public partial class CreateDBAndTableScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblErrorLog",
                columns: table => new
                {
                    ErrorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    ControllerName = table.Column<string>(type: "varchar(50)", nullable: false),
                    ActionName = table.Column<string>(type: "varchar(50)", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JSONRequest = table.Column<string>(type: "varchar(max)", nullable: false),
                    ErrorStackTrace = table.Column<string>(type: "varchar(max)", nullable: false),
                    ClientIP = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErrorLog", x => x.ErrorId);
                });

            migrationBuilder.CreateTable(
                name: "tblUserPost",
                columns: table => new
                {
                    UserPostId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    PostedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostType = table.Column<string>(type: "varchar(100)", nullable: false),
                    PostText = table.Column<string>(type: "varchar(max)", nullable: false),
                    PostMediaPath = table.Column<string>(type: "varchar(500)", nullable: false),
                    LikeCount = table.Column<long>(type: "bigint", nullable: true),
                    CommentCount = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserPost", x => x.UserPostId);
                });

            migrationBuilder.CreateTable(
                name: "tblUserPostComment",
                columns: table => new
                {
                    UserPostCommentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    UserPostId = table.Column<long>(type: "bigint", nullable: true),
                    CommentText = table.Column<string>(type: "varchar(max)", nullable: false),
                    CommentedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserPostComment", x => x.UserPostCommentId);
                });

            migrationBuilder.CreateTable(
                name: "tblUserPostLike",
                columns: table => new
                {
                    UserPostLikeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    UserPostId = table.Column<long>(type: "bigint", nullable: true),
                    LikedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserPostLike", x => x.UserPostLikeId);
                });

            migrationBuilder.CreateTable(
                name: "tblUsersMaster",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(100)", nullable: false),
                    Password = table.Column<string>(type: "varchar(500)", nullable: false),
                    Firstname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Lastname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Mobile = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<string>(type: "char(1)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfilePicPath = table.Column<string>(type: "varchar(500)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsersMaster", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblErrorLog");

            migrationBuilder.DropTable(
                name: "tblUserPost");

            migrationBuilder.DropTable(
                name: "tblUserPostComment");

            migrationBuilder.DropTable(
                name: "tblUserPostLike");

            migrationBuilder.DropTable(
                name: "tblUsersMaster");
        }
    }
}
