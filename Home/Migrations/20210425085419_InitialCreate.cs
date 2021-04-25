using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Home.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typeid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_typeid",
                        column: x => x.typeid,
                        principalTable: "UserTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Locations_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "text", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: false),
                    locationid = table.Column<int>(type: "int", nullable: false),
                    deadline = table.Column<DateTime>(type: "date", nullable: false),
                    budget = table.Column<decimal>(type: "decimal", nullable: false),
                    categoryid = table.Column<int>(type: "int", nullable: false),
                    statusid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tasks_Categories_categoryid",
                        column: x => x.categoryid,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Locations_locationid",
                        column: x => x.locationid,
                        principalTable: "Locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Statuses_statusid",
                        column: x => x.statusid,
                        principalTable: "Statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "id", "type" },
                values: new object[,]
                {
                    { 1, "Cleaning and disinfection" },
                    { 2, "Care for pets and plants" },
                    { 3, "Child care" },
                    { 4, "Care for the elderly" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "id", "status" },
                values: new object[,]
                {
                    { 1, "Waiting" },
                    { 2, "Appointed as a domestic helper" },
                    { 3, "Fulfilled" },
                    { 4, "Refused" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "id", "type" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 2, "Housekeeper" },
                    { 3, "Client" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_name",
                table: "Locations",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_userid",
                table: "Locations",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_categoryid",
                table: "Tasks",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_locationid",
                table: "Tasks",
                column: "locationid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_statusid",
                table: "Tasks",
                column: "statusid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_userid",
                table: "Tasks",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_typeid",
                table: "Users",
                column: "typeid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_username",
                table: "Users",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
