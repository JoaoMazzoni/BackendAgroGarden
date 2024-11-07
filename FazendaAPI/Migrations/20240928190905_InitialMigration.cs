using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FazendaAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plantacao",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalDePlantio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataDePlantio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Irrigacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LuzSolar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CondicaoClimatica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Crescimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Colheita = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plantacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Praga",
                columns: table => new
                {
                    PragaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeDaPraga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoPraga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoControle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ControleAdequado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Praga", x => x.PragaId);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    CNPJ = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnderecoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.CNPJ);
                    table.ForeignKey(
                        name: "FK_Cliente_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    CNPJ = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeDoFornecedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDeFornecimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnderecoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.CNPJ);
                    table.ForeignKey(
                        name: "FK_Fornecedor_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnderecoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.CPF);
                    table.ForeignKey(
                        name: "FK_Usuario_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Colheita",
                columns: table => new
                {
                    NumeroRegistro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataColheita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantidadeColhida = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colheita", x => x.NumeroRegistro);
                    table.ForeignKey(
                        name: "FK_Colheita_Plantacao_PlantacaoId",
                        column: x => x.PlantacaoId,
                        principalTable: "Plantacao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RegistroInfestacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantacaoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PragaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataConclusaoTratamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cauterizado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroInfestacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroInfestacao_Plantacao_PlantacaoId",
                        column: x => x.PlantacaoId,
                        principalTable: "Plantacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroInfestacao_Praga_PragaId",
                        column: x => x.PragaId,
                        principalTable: "Praga",
                        principalColumn: "PragaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insumo",
                columns: table => new
                {
                    CodigoLote = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeDoInsumo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Funcao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FornecedorCNPJ = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QuantidadeEntrada = table.Column<int>(type: "int", nullable: false),
                    MililitrosAtual = table.Column<int>(type: "int", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumo", x => x.CodigoLote);
                    table.ForeignKey(
                        name: "FK_Insumo_Fornecedor_FornecedorCNPJ",
                        column: x => x.FornecedorCNPJ,
                        principalTable: "Fornecedor",
                        principalColumn: "CNPJ");
                });

            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteCNPJ = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FuncionarioCPF = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataDaVenda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormaPagamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venda_Cliente_ClienteCNPJ",
                        column: x => x.ClienteCNPJ,
                        principalTable: "Cliente",
                        principalColumn: "CNPJ");
                    table.ForeignKey(
                        name: "FK_Venda_Usuario_FuncionarioCPF",
                        column: x => x.FuncionarioCPF,
                        principalTable: "Usuario",
                        principalColumn: "CPF");
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Lote = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriaProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColheitaOrigemNumeroRegistro = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<long>(type: "bigint", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Lote);
                    table.ForeignKey(
                        name: "FK_Produto_Colheita_ColheitaOrigemNumeroRegistro",
                        column: x => x.ColheitaOrigemNumeroRegistro,
                        principalTable: "Colheita",
                        principalColumn: "NumeroRegistro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AplicacaoInsumo",
                columns: table => new
                {
                    Registro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InsumoCodigoLote = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    DataAplicacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacaoInsumo", x => x.Registro);
                    table.ForeignKey(
                        name: "FK_AplicacaoInsumo_Insumo_InsumoCodigoLote",
                        column: x => x.InsumoCodigoLote,
                        principalTable: "Insumo",
                        principalColumn: "CodigoLote",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AplicacaoInsumo_Plantacao_PlantacaoId",
                        column: x => x.PlantacaoId,
                        principalTable: "Plantacao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProdutoVenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoLote = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quantidade = table.Column<long>(type: "bigint", nullable: false),
                    VendaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoVenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoVenda_Produto_ProdutoLote",
                        column: x => x.ProdutoLote,
                        principalTable: "Produto",
                        principalColumn: "Lote");
                    table.ForeignKey(
                        name: "FK_ProdutoVenda_Venda_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Venda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Praga",
                columns: new[] { "PragaId", "ControleAdequado", "NomeDaPraga", "TipoControle", "TipoPraga" },
                values: new object[,]
                {
                    { "001", "Inseticida", "Pulgões", "Químico", "Inseto" },
                    { "002", "Inseticida", "Lagartas", "Químico", "Inseto" },
                    { "003", "Inseticida", "Cochonilhas", "Químico", "Inseto" },
                    { "004", "Barreiras físicas, remoção manual", "Lesmas", "Cultural", "Molusco" },
                    { "005", "Barreiras físicas, remoção manual", "Caracóis", "Cultural", "Molusco" },
                    { "006", "Inseticida", "Moscas", "Químico", "Inseto" },
                    { "007", "Inseticida", "Mosquitos", "Químico", "Inseto" },
                    { "008", "Inseticida", "Trips", "Químico", "Inseto" },
                    { "009", "Ácaros predadores", "Ácaros", "Biológico", "Aracnídeo" },
                    { "010", "Inseticida", "Besouros", "Químico", "Inseto" },
                    { "011", "Inseticida", "Formigas", "Químico", "Inseto" },
                    { "012", "Rotação de culturas, solarização do solo", "Nematóides", "Cultural", "Verme" },
                    { "013", "Rotação de culturas, remoção de plantas infestadas", "Cigarras", "Cultural", "Inseto" },
                    { "014", "Inseticida", "Brocas", "Químico", "Inseto" },
                    { "015", "Inseticida", "Percevejos", "Químico", "Inseto" },
                    { "016", "Manejo adequado do solo, cobertura do solo", "Grilos", "Cultural", "Inseto" },
                    { "017", "Rotação de culturas, manutenção de áreas limpas", "Gafanhotos", "Cultural", "Inseto" },
                    { "018", "Plantio em sistemas que dificultem o acesso, uso de barreiras", "Minhocas Cortadeiras", "Cultural", "Inseto" },
                    { "019", "Manutenção adequada da estrutura de madeira, remoção de madeiras deterioradas", "Bicho Carpinteiro", "Cultural", "Inseto" },
                    { "020", "Rotação de culturas, uso de solo saudável", "Vermes", "Cultural", "Verme" },
                    { "021", "Inseticida", "Psilídeos", "Químico", "Inseto" },
                    { "022", "Inseticida", "Cicadelídeos", "Químico", "Inseto" },
                    { "023", "Manutenção de áreas limpas, uso de armadilhas", "Bicho da Luz", "Cultural", "Inseto" },
                    { "024", "Uso de predadores naturais", "Aranhas", "Biológico", "Aracnídeo" },
                    { "025", "Inseticida", "Gorgulhos", "Químico", "Inseto" },
                    { "026", "Fungicida", "Oídio", "Químico", "Fungo" },
                    { "027", "Fungicida", "Ferrugem", "Químico", "Fungo" },
                    { "028", "Fungicida", "Míldio", "Químico", "Fungo" },
                    { "029", "Fungicida", "Antracnose", "Químico", "Fungo" },
                    { "030", "Fungicida", "Fusarinose", "Químico", "Fungo" },
                    { "031", "Fungicida", "Mofo", "Químico", "Fungo" },
                    { "032", "Fungicida", "Cytospora", "Químico", "Fungo" },
                    { "033", "Fungicida", "Mancha Foliar", "Químico", "Fungo" },
                    { "034", "Fungicida", "Podridão", "Químico", "Fungo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoInsumo_InsumoCodigoLote",
                table: "AplicacaoInsumo",
                column: "InsumoCodigoLote");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoInsumo_PlantacaoId",
                table: "AplicacaoInsumo",
                column: "PlantacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_EnderecoId",
                table: "Cliente",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Colheita_PlantacaoId",
                table: "Colheita",
                column: "PlantacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_EnderecoId",
                table: "Fornecedor",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_FornecedorCNPJ",
                table: "Insumo",
                column: "FornecedorCNPJ");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_ColheitaOrigemNumeroRegistro",
                table: "Produto",
                column: "ColheitaOrigemNumeroRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoVenda_ProdutoLote",
                table: "ProdutoVenda",
                column: "ProdutoLote");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoVenda_VendaId",
                table: "ProdutoVenda",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroInfestacao_PlantacaoId",
                table: "RegistroInfestacao",
                column: "PlantacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroInfestacao_PragaId",
                table: "RegistroInfestacao",
                column: "PragaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EnderecoId",
                table: "Usuario",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_ClienteCNPJ",
                table: "Venda",
                column: "ClienteCNPJ");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_FuncionarioCPF",
                table: "Venda",
                column: "FuncionarioCPF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AplicacaoInsumo");

            migrationBuilder.DropTable(
                name: "ProdutoVenda");

            migrationBuilder.DropTable(
                name: "RegistroInfestacao");

            migrationBuilder.DropTable(
                name: "Insumo");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Venda");

            migrationBuilder.DropTable(
                name: "Praga");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Colheita");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Plantacao");

            migrationBuilder.DropTable(
                name: "Endereco");
        }
    }
}
