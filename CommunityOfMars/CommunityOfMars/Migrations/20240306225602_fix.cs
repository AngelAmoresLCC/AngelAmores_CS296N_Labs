using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityOfMars.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_Parent",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "Parent",
                table: "Messages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_Parent",
                table: "Messages",
                column: "Parent",
                principalTable: "Messages",
                principalColumn: "MessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_Parent",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "Parent",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_Parent",
                table: "Messages",
                column: "Parent",
                principalTable: "Messages",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
