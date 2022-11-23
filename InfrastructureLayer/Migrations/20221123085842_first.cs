using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRegisterDetails",
                columns: table => new
                {
                    Username = table.Column<string>(type: "VARCHAR(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(25)", nullable: true),
                    PhoneNumber = table.Column<int>(type: "INT", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "VARBINARY(64)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "VARBINARY(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRegisterDetails", x => x.Username);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRegisterDetails");
        }
    }
}
