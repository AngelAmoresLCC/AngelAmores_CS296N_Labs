using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityOfMars.Migrations
{
    public partial class Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_Parent",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_Parent",
                table: "Messages",
                column: "Parent",
                principalTable: "Messages",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_Parent",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_Parent",
                table: "Messages",
                column: "Parent",
                principalTable: "Messages",
                principalColumn: "MessageId");
        }
    }
}
