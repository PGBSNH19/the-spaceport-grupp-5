using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSpaceport.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_LargeShips_LargeShipShipID",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_SmallShips_SmallShipShipID",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "LargeShips");

            migrationBuilder.DropTable(
                name: "SmallShips");

            migrationBuilder.DropIndex(
                name: "IX_Persons_LargeShipShipID",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_SmallShipShipID",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "LargeShipShipID",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "SmallShipShipID",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "SpaceshipShipID",
                table: "Persons",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Spaceships",
                columns: table => new
                {
                    ShipID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipName = table.Column<string>(nullable: true),
                    NumberOfDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaceships", x => x.ShipID);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Spaceships_SpaceshipShipID",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "Spaceships");

            migrationBuilder.DropIndex(
                name: "IX_Persons_SpaceshipShipID",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "SpaceshipShipID",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "LargeShipShipID",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SmallShipShipID",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LargeShips",
                columns: table => new
                {
                    ShipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    ShipName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LargeShips", x => x.ShipID);
                });

            migrationBuilder.CreateTable(
                name: "SmallShips",
                columns: table => new
                {
                    ShipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    ShipName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmallShips", x => x.ShipID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_LargeShipShipID",
                table: "Persons",
                column: "LargeShipShipID");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_SmallShipShipID",
                table: "Persons",
                column: "SmallShipShipID");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_LargeShips_LargeShipShipID",
                table: "Persons",
                column: "LargeShipShipID",
                principalTable: "LargeShips",
                principalColumn: "ShipID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_SmallShips_SmallShipShipID",
                table: "Persons",
                column: "SmallShipShipID",
                principalTable: "SmallShips",
                principalColumn: "ShipID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
