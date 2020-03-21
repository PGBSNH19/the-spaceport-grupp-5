using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSpaceport.Migrations
{
    public partial class ChangeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Houses_HouseID",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Persons_HouseID",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "HouseID",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "ShipID",
                table: "Persons",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    ShipID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipName = table.Column<string>(nullable: true),
                    ShipLength = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.ShipID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ShipID",
                table: "Persons",
                column: "ShipID");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Ships_ShipID",
                table: "Persons",
                column: "ShipID",
                principalTable: "Ships",
                principalColumn: "ShipID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Ships_ShipID",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ShipID",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ShipID",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "HouseID",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    HouseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.HouseID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_HouseID",
                table: "Persons",
                column: "HouseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Houses_HouseID",
                table: "Persons",
                column: "HouseID",
                principalTable: "Houses",
                principalColumn: "HouseID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
