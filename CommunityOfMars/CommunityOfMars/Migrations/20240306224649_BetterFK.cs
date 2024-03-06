using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityOfMars.Migrations
{
    public partial class BetterFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_MessageId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageId1",
                table: "Messages");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Messages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Parent",
                table: "Messages",
                column: "Parent");

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

            migrationBuilder.DropIndex(
                name: "IX_Messages_Parent",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "MessageId1",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageId1",
                table: "Messages",
                column: "MessageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_MessageId1",
                table: "Messages",
                column: "MessageId1",
                principalTable: "Messages",
                principalColumn: "MessageId");
        }
    }
}
