using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractService.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "contracts",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                id_proposal = table.Column<Guid>(type: "uuid", nullable: false),
                sign_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_contracts", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "contracts");
    }
}
