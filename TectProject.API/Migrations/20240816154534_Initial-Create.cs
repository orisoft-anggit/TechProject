using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TectProject.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MsStorageLocations",
                columns: table => new
                {
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsStorageLocations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "MsUsers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "TrBpkbs",
                columns: table => new
                {
                    AgreementNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BpkbNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BpkbDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FakturNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FakturDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PoliceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BpkbDateIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrBpkbs", x => x.AgreementNumber);
                    table.ForeignKey(
                        name: "FK_TrBpkbs_MsStorageLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "MsStorageLocations",
                        principalColumn: "LocationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrBpkbs_LocationId",
                table: "TrBpkbs",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MsUsers");

            migrationBuilder.DropTable(
                name: "TrBpkbs");

            migrationBuilder.DropTable(
                name: "MsStorageLocations");
        }
    }
}
