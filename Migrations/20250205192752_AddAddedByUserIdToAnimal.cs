using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdoptionPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddAddedByUserIdToAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AspNetUsers_AddedByUserId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_AddedByUserId",
                table: "Animals");

            migrationBuilder.AlterColumn<string>(
                name: "AddedByUserId",
                table: "Animals",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "AddedByUserId1",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_AddedByUserId1",
                table: "Animals",
                column: "AddedByUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AspNetUsers_AddedByUserId1",
                table: "Animals",
                column: "AddedByUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AspNetUsers_AddedByUserId1",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_AddedByUserId1",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "AddedByUserId1",
                table: "Animals");

            migrationBuilder.AlterColumn<int>(
                name: "AddedByUserId",
                table: "Animals",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_AddedByUserId",
                table: "Animals",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AspNetUsers_AddedByUserId",
                table: "Animals",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
