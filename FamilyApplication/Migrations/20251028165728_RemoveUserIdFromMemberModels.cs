using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyApplication.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserIdFromMemberModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberModels_RegisterModels_UserId",
                table: "MemberModels");

            migrationBuilder.DropIndex(
                name: "IX_MemberModels_UserId",
                table: "MemberModels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MemberModels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MemberModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MemberModels_UserId",
                table: "MemberModels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberModels_RegisterModels_UserId",
                table: "MemberModels",
                column: "UserId",
                principalTable: "RegisterModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
