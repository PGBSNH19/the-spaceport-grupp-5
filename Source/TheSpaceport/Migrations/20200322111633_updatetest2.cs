using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSpaceport.Migrations
{
    public partial class updatetest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Spaceships_SpaceshipShipID",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_SpaceshipShipID",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "SpaceshipShipID",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "Spaceships",
                nullable: true);

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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spaceships_Persons_PersonID",
                table: "Spaceships");

            migrationBuilder.DropIndex(
                name: "IX_Spaceships_PersonID",
                table: "Spaceships");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Spaceships");

            migrationBuilder.AddColumn<int>(
                name: "SpaceshipShipID",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_SpaceshipShipID",
                table: "Persons",
                column: "SpaceshipShipID");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Spaceships_SpaceshipShipID",
                table: "Persons",
                column: "SpaceshipShipID",
                principalTable: "Spaceships",
                principalColumn: "ShipID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
