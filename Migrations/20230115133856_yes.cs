using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShopWeb.Migrations
{
    public partial class yes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HairstylistID",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Appointment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_CategoryID",
                table: "Appointment",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Category_CategoryID",
                table: "Appointment",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Category_CategoryID",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_CategoryID",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Appointment");

            migrationBuilder.AlterColumn<int>(
                name: "HairstylistID",
                table: "Appointment",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
