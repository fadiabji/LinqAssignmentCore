using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinqAssignmentCore.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Engine_EngineId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engine",
                table: "Engine");

            migrationBuilder.RenameTable(
                name: "Engine",
                newName: "Engines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engines",
                table: "Engines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Engines_EngineId",
                table: "Cars",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Engines_EngineId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engines",
                table: "Engines");

            migrationBuilder.RenameTable(
                name: "Engines",
                newName: "Engine");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engine",
                table: "Engine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Engine_EngineId",
                table: "Cars",
                column: "EngineId",
                principalTable: "Engine",
                principalColumn: "Id");
        }
    }
}
