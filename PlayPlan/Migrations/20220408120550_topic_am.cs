using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayPlan.Migrations
{
    public partial class topic_am : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TopicCommentsAmount",
                table: "Topics",
                newName: "TopicCommentsAmount2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TopicCommentsAmount2",
                table: "Topics",
                newName: "TopicCommentsAmount");
        }
    }
}
