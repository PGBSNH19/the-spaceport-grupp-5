using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSpaceport.Migrations
{
    public partial class testadd1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Spaceships_StartshipShipID",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_StartshipShipID",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "StartshipShipID",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "DataBasePersonPersonID",
                table: "Spaceships",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "StartshipShipID",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_StartshipShipID",
                table: "Persons",
                column: "StartshipShipID");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Spaceships_StartshipShipID",
                table: "Persons",
                column: "StartshipShipID",
                principalTable: "Spaceships",
                principalColumn: "ShipID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
