using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSpaceport.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spaceships_Persons_DataBasePersonPersonID",
                table: "Spaceships");

            migrationBuilder.DropIndex(
                name: "IX_Spaceships_DataBasePersonPersonID",
                table: "Spaceships");

            migrationBuilder.DropColumn(
                name: "DataBasePersonPersonID",
                table: "Spaceships");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataBasePersonPersonID",
                table: "Spaceships",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spaceships_DataBasePersonPersonID",
                table: "Spaceships",
                column: "DataBasePersonPersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Spaceships_Persons_DataBasePersonPersonID",
                table: "Spaceships",
                column: "DataBasePersonPersonID",
                principalTable: "Persons",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
