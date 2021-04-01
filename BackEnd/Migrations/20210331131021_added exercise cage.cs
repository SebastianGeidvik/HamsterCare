using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class addedexercisecage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExerciseCageId",
                table: "Hamsters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExerciseCages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseCages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hamsters_ExerciseCageId",
                table: "Hamsters",
                column: "ExerciseCageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hamsters_ExerciseCages_ExerciseCageId",
                table: "Hamsters",
                column: "ExerciseCageId",
                principalTable: "ExerciseCages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hamsters_ExerciseCages_ExerciseCageId",
                table: "Hamsters");

            migrationBuilder.DropTable(
                name: "ExerciseCages");

            migrationBuilder.DropIndex(
                name: "IX_Hamsters_ExerciseCageId",
                table: "Hamsters");

            migrationBuilder.DropColumn(
                name: "ExerciseCageId",
                table: "Hamsters");
        }
    }
}
