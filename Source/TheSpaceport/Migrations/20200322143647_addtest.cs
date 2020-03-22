using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSpaceport.Migrations
{
    public partial class addtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Spaceships_DataBaseStarthipShipID",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_DataBaseStarthipShipID",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "DataBaseStarthipShipID",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "StartshipShipID",
                table: "Persons",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "DataBaseStarthipShipID",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DataBaseStarthipShipID",
                table: "Persons",
                column: "DataBaseStarthipShipID");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Spaceships_DataBaseStarthipShipID",
                table: "Persons",
                column: "DataBaseStarthipShipID",
                principalTable: "Spaceships",
                principalColumn: "ShipID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
