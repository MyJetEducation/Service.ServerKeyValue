using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.ServerKeyValue.Postgres.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "education");

            migrationBuilder.CreateTable(
                name: "serverkeyvalue",
                schema: "education",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serverkeyvalue", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_serverkeyvalue_UserId_Key",
                schema: "education",
                table: "serverkeyvalue",
                columns: new[] { "UserId", "Key" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "serverkeyvalue",
                schema: "education");
        }
    }
}
