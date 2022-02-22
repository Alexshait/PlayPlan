using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayPlan.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonName = table.Column<string>(type: "TEXT", nullable: false),
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
                    TopicParsePhrases = table.Column<string>(type: "TEXT", nullable: true),
                    VkApiVer = table.Column<string>(type: "TEXT", nullable: true),
                    ApiUrl = table.Column<string>(type: "TEXT", nullable: true),
                    GroupName = table.Column<string>(type: "TEXT", nullable: true),
                    PersonFieldName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    TopicID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TopicTitle = table.Column<string>(type: "TEXT", nullable: true),
                    TopicCreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    TopicUpdatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    TopicDateCreate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TopicDateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TopicCommentsAmount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.TopicID);
                });

            migrationBuilder.CreateTable(
                name: "TopicComments",
                columns: table => new
                {
                    Topic_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateComment = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PersonID = table.Column<int>(type: "INTEGER", nullable: false),
                    TopicFrom = table.Column<string>(type: "TEXT", nullable: true),
                    Participants = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateInput = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TopicID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicComments", x => x.Topic_ID);
                    table.ForeignKey(
                        name: "FK_TopicComments_Topics_TopicID",
                        column: x => x.TopicID,
                        principalTable: "Topics",
                        principalColumn: "TopicID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicComments_TopicID",
                table: "TopicComments",
                column: "TopicID");
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
