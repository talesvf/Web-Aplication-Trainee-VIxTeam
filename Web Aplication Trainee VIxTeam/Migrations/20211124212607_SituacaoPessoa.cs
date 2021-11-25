using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Aplication_Trainee_VIxTeam.Migrations
{
    public partial class SituacaoPessoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Situacao",
                table: "PessoaModel",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "PessoaModel");
        }
    }
}
