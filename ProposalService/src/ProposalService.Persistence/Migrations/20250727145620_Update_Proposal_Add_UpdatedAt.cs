using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProposalService.Persistence.Migrations;

/// <inheritdoc />
public partial class Update_Proposal_Add_UpdatedAt : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "updated_at",
            table: "proposals",
            type: "timestamp without time zone",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "updated_at",
            table: "proposals");
    }
}
