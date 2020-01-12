using Microsoft.EntityFrameworkCore.Migrations;

namespace Phonebook.Migrations
{
    public partial class PhoneBookv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_LastName",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Phones",
                newName: "person_id");

            migrationBuilder.RenameIndex(
                name: "IX_Phones_PersonId",
                table: "Phones",
                newName: "IX_Phones_person_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Persons",
                newName: "description");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "Persons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_first_name",
                table: "Persons",
                column: "first_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_first_name",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "Phones",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Phones_person_id",
                table: "Phones",
                newName: "IX_Phones_PersonId");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Persons",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_LastName",
                table: "Persons",
                column: "LastName");
        }
    }
}
