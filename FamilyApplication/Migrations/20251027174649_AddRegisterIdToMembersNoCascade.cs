using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddRegisterIdToMembersNoCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegisterId",
                table: "MemberModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MemberModels_RegisterId",
                table: "MemberModels",
                column: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberModels_RegisterModels_RegisterId",
                table: "MemberModels",
                column: "RegisterId",
                principalTable: "RegisterModels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberModels_RegisterModels_RegisterId",
                table: "MemberModels");

            migrationBuilder.DropIndex(
                name: "IX_MemberModels_RegisterId",
                table: "MemberModels");

            migrationBuilder.DropColumn(
                name: "RegisterId",
                table: "MemberModels");
        }
    }
}
