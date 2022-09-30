using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiToken.Migrations
{
    public partial class CriandoTabelasFilmes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "filmesModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cadastro = table.Column<string>(nullable: true),
                    Voto = table.Column<string>(nullable: true),
                    Listagem = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filmesModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "filmesModels");
        }
    }
}
