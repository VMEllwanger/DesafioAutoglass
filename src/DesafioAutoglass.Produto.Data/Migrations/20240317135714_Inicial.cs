using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioAutoglass.Produtos.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "SequenciaCodigoFornecedor",
                startValue: 1000L);

            migrationBuilder.CreateSequence<int>(
                name: "SequenciaCodigoProdutos",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SequenciaCodigoFornecedor"),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(18)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoProduto = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SequenciaCodigoProdutos"),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataDeFabricacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DataDeValidade = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CodigoFornecedor = table.Column<string>(type: "varchar(100)", nullable: true),
                    DescricaoFornecedor = table.Column<string>(type: "varchar(100)", nullable: true),
                    CNPJFornecedor = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FornecedorId",
                table: "Produtos",
                column: "FornecedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropSequence(
                name: "SequenciaCodigoFornecedor");

            migrationBuilder.DropSequence(
                name: "SequenciaCodigoProdutos");
        }
    }
}
