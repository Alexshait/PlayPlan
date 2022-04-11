using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayPlan.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonName = table.Column<string>(type: "TEXT", nullable: true),
                    ParsePhrases = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApiID = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupID = table.Column<int>(type: "INTEGER", nullable: false),
                    VkApiVer = table.Column<string>(type: "TEXT", nullable: true),
                    ApiUrl = table.Column<string>(type: "TEXT", nullable: true),
                    GroupName = table.Column<string>(type: "TEXT", nullable: true),
                    WindowWidth = table.Column<string>(type: "TEXT", nullable: true),
                    WindowHeigth = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TopicID = table.Column<int>(type: "INTEGER", nullable: false),
                    TopicTitle = table.Column<string>(type: "TEXT", nullable: true),
                    TopicCreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    TopicUpdatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    TopicDateCreate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TopicDateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TopicCommentsAmount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopicComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Topic_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    DateComment = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PersonID = table.Column<int>(type: "INTEGER", nullable: false),
                    CommentID = table.Column<int>(type: "INTEGER", nullable: false),
                    CommentFrom = table.Column<string>(type: "TEXT", nullable: true),
                    Participants = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateInput = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TopicId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicComments_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ID",
                table: "Persons",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Settings_ID",
                table: "Settings",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopicComments_TopicId",
                table: "TopicComments",
                column: "TopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "TopicComments");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
