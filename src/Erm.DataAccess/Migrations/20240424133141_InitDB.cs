using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Erm.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "business_process",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    domain = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_business_process", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "risk_profile",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    risk_name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    description = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    occurrence_probability = table.Column<int>(type: "INTEGER", nullable: false),
                    potential_business_impact = table.Column<int>(type: "INTEGER", nullable: false),
                    potential_solution = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    business_process_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_risk_profile", x => x.id);
                    table.ForeignKey(
                        name: "FK_risk_profile_business_process_business_process_id",
                        column: x => x.business_process_id,
                        principalTable: "business_process",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "risk",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    type = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    description = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    time_frame = table.Column<int>(type: "INTEGER", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: true),
                    risk_profile_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_risk", x => x.id);
                    table.ForeignKey(
                        name: "FK_risk_risk_profile_risk_profile_id",
                        column: x => x.risk_profile_id,
                        principalTable: "risk_profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    risk_id = table.Column<int>(type: "INTEGER", nullable: true),
                    risk_profile_id = table.Column<int>(type: "INTEGER", nullable: true),
                    business_process_id = table.Column<int>(type: "INTEGER", nullable: true),
                    message = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    is_read = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification", x => x.id);
                    table.ForeignKey(
                        name: "FK_notification_business_process_business_process_id",
                        column: x => x.business_process_id,
                        principalTable: "business_process",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notification_risk_profile_risk_profile_id",
                        column: x => x.risk_profile_id,
                        principalTable: "risk_profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notification_risk_risk_id",
                        column: x => x.risk_id,
                        principalTable: "risk",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_business_process_name",
                table: "business_process",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notification_business_process_id",
                table: "notification",
                column: "business_process_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_risk_id",
                table: "notification",
                column: "risk_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_risk_profile_id",
                table: "notification",
                column: "risk_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_risk_name",
                table: "risk",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_risk_risk_profile_id",
                table: "risk",
                column: "risk_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_risk_profile_business_process_id",
                table: "risk_profile",
                column: "business_process_id");

            migrationBuilder.CreateIndex(
                name: "IX_risk_profile_risk_name",
                table: "risk_profile",
                column: "risk_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "risk");

            migrationBuilder.DropTable(
                name: "risk_profile");

            migrationBuilder.DropTable(
                name: "business_process");
        }
    }
}
