using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSpaceport.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Spaceships_PersonID",
                table: "Spaceships",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Spaceships_Persons_PersonID",
                table: "Spaceships",
                column: "PersonID",
                principalTable: "Persons",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spaceships_Persons_PersonID",
                table: "Spaceships");

            migrationBuilder.DropIndex(
                name: "IX_Spaceships_PersonID",
                table: "Spaceships");
        }
    }
}
