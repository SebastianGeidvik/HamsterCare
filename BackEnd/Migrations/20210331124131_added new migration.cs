using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class addednewmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CageId",
                table: "Hamsters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hamsters_CageId",
                table: "Hamsters",
                column: "CageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hamsters_Cages_CageId",
                table: "Hamsters",
                column: "CageId",
                principalTable: "Cages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hamsters_Cages_CageId",
                table: "Hamsters");

            migrationBuilder.DropIndex(
                name: "IX_Hamsters_CageId",
                table: "Hamsters");

            migrationBuilder.DropColumn(
                name: "CageId",
                table: "Hamsters");
        }
    }
}
