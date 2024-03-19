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
                    DataCadastro = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "GETDATE()")
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
                    Codigo = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SequenciaCodigoProdutos"),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataDeFabricacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DataDeValidade = table.Column<DateTime>(type: "DATETIME", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "CNPJ", "Descricao" },
                values: new object[] { new Guid("de5f43de-21d0-4b88-ac0a-0ab368d2f8aa"), "60585144000189", "Emporio10 ME" });

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "CNPJ", "Descricao" },
                values: new object[] { new Guid("3f4e05ba-a16a-4232-98e6-b6f89575f58a"), "69223161000140", "Pães e Doces Ltda" });

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "CNPJ", "Descricao" },
                values: new object[] { new Guid("96f1fb45-3e72-4f42-8032-0f9441e6f5db"), "49526608000143", "Massa e Borda Pizzaria ME" });

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
