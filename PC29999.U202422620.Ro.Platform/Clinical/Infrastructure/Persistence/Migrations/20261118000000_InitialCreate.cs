using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;
using PC29999.U202422620.Ro.Platform.Clinical.Infrastructure.Persistence;

namespace PC29999.U202422620.Ro.Platform.Clinical.Infrastructure.Persistence.Migrations;

[DbContext(typeof(PrescriptionDbContext))]
[Migration("20261118000000_InitialCreate")]
public sealed class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "prescriptions",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                prescription_number = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false),
                patient_id = table.Column<Guid>(type: "char(36)", nullable: false),
                start_date = table.Column<DateOnly>(type: "date", nullable: false),
                end_date = table.Column<DateOnly>(type: "date", nullable: false),
                daily_quantity = table.Column<int>(type: "int", nullable: false),
                active_ingredient = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                clinical_notes = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_prescriptions", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_prescriptions_prescription_number",
            table: "prescriptions",
            column: "prescription_number",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "prescriptions");
    }
}
