
using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace FazendaAPI.Data
{
    public class FazendaAPIContext : DbContext
    {
        public FazendaAPIContext(DbContextOptions<FazendaAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Plantacao> Plantacao { get; set; } = default!;
        public DbSet<Fornecedor> Fornecedor { get; set; } = default!;
        public DbSet<Insumo> Insumo { get; set; } = default!;
        public DbSet<Endereco> Endereco { get; set; } = default!;
        public DbSet<AplicacaoInsumo> AplicacaoInsumo { get; set; } = default!;
        public DbSet<Praga> Praga { get; set; } = default!;
        public DbSet<RegistroInfestacao> RegistroInfestacao { get; set; } = default!;
        public DbSet<Colheita> Colheita { get; set; } = default!;
        public DbSet<Produto>? Produto { get; set; } = default!;
        public DbSet<Venda> Venda { get; set; } = default!;
        public DbSet<ProdutoVenda> ProdutoVenda { get; set; } = default!;
        public DbSet<Usuario> Usuario { get; set; } = default!;
        public DbSet<Cliente> Cliente { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Praga>().HasData(
                new Praga { PragaId = "001", NomeDaPraga = "Pulgões", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "002", NomeDaPraga = "Lagartas", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "003", NomeDaPraga = "Cochonilhas", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "004", NomeDaPraga = "Lesmas", TipoPraga = "Molusco", TipoControle = "Cultural", ControleAdequado = "Barreiras físicas, remoção manual" },
                new Praga { PragaId = "005", NomeDaPraga = "Caracóis", TipoPraga = "Molusco", TipoControle = "Cultural", ControleAdequado = "Barreiras físicas, remoção manual" },
                new Praga { PragaId = "006", NomeDaPraga = "Moscas", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "007", NomeDaPraga = "Mosquitos", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "008", NomeDaPraga = "Trips", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "009", NomeDaPraga = "Ácaros", TipoPraga = "Aracnídeo", TipoControle = "Biológico", ControleAdequado = "Ácaros predadores" },
                new Praga { PragaId = "010", NomeDaPraga = "Besouros", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "011", NomeDaPraga = "Formigas", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "012", NomeDaPraga = "Nematóides", TipoPraga = "Verme", TipoControle = "Cultural", ControleAdequado = "Rotação de culturas, solarização do solo" },
                new Praga { PragaId = "013", NomeDaPraga = "Cigarras", TipoPraga = "Inseto", TipoControle = "Cultural", ControleAdequado = "Rotação de culturas, remoção de plantas infestadas" },
                new Praga { PragaId = "014", NomeDaPraga = "Brocas", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "015", NomeDaPraga = "Percevejos", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "016", NomeDaPraga = "Grilos", TipoPraga = "Inseto", TipoControle = "Cultural", ControleAdequado = "Manejo adequado do solo, cobertura do solo" },
                new Praga { PragaId = "017", NomeDaPraga = "Gafanhotos", TipoPraga = "Inseto", TipoControle = "Cultural", ControleAdequado = "Rotação de culturas, manutenção de áreas limpas" },
                new Praga { PragaId = "018", NomeDaPraga = "Minhocas Cortadeiras", TipoPraga = "Inseto", TipoControle = "Cultural", ControleAdequado = "Plantio em sistemas que dificultem o acesso, uso de barreiras" },
                new Praga { PragaId = "019", NomeDaPraga = "Bicho Carpinteiro", TipoPraga = "Inseto", TipoControle = "Cultural", ControleAdequado = "Manutenção adequada da estrutura de madeira, remoção de madeiras deterioradas" },
                new Praga { PragaId = "020", NomeDaPraga = "Vermes", TipoPraga = "Verme", TipoControle = "Cultural", ControleAdequado = "Rotação de culturas, uso de solo saudável" },
                new Praga { PragaId = "021", NomeDaPraga = "Psilídeos", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "022", NomeDaPraga = "Cicadelídeos", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "023", NomeDaPraga = "Bicho da Luz", TipoPraga = "Inseto", TipoControle = "Cultural", ControleAdequado = "Manutenção de áreas limpas, uso de armadilhas" },
                new Praga { PragaId = "024", NomeDaPraga = "Aranhas", TipoPraga = "Aracnídeo", TipoControle = "Biológico", ControleAdequado = "Uso de predadores naturais" },
                new Praga { PragaId = "025", NomeDaPraga = "Gorgulhos", TipoPraga = "Inseto", TipoControle = "Químico", ControleAdequado = "Inseticida" },
                new Praga { PragaId = "026", NomeDaPraga = "Oídio", TipoPraga = "Fungo", TipoControle = "Químico", ControleAdequado = "Fungicida" },
                new Praga { PragaId = "027", NomeDaPraga = "Ferrugem", TipoPraga = "Fungo", TipoControle = "Químico", ControleAdequado = "Fungicida" },
                new Praga { PragaId = "028", NomeDaPraga = "Míldio", TipoPraga = "Fungo", TipoControle = "Químico", ControleAdequado = "Fungicida" },
                new Praga { PragaId = "029", NomeDaPraga = "Antracnose", TipoPraga = "Fungo", TipoControle = "Químico", ControleAdequado = "Fungicida" },
                new Praga { PragaId = "030", NomeDaPraga = "Fusarinose", TipoPraga = "Fungo", TipoControle = "Químico", ControleAdequado = "Fungicida" },
                new Praga { PragaId = "031", NomeDaPraga = "Mofo", TipoPraga = "Fungo", TipoControle = "Químico", ControleAdequado = "Fungicida" },
                new Praga { PragaId = "032", NomeDaPraga = "Cytospora", TipoPraga = "Fungo", TipoControle = "Químico", ControleAdequado = "Fungicida" },
                new Praga { PragaId = "033", NomeDaPraga = "Mancha Foliar", TipoPraga = "Fungo", TipoControle = "Químico", ControleAdequado = "Fungicida" },
                new Praga { PragaId = "034", NomeDaPraga = "Podridão", TipoPraga = "Fungo", TipoControle = "Químico", ControleAdequado = "Fungicida" }
            );

            modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Endereco)
            .WithMany()
            .HasForeignKey("EnderecoId");

            modelBuilder.Entity<Endereco>().HasData(
                new Endereco
                {
                    Id = "14820768140",
                    Rua = "Rua Itirapina",
                    Numero = "140",
                    Bairro = "COHAB",
                    Cidade = "Américo Brasiliense",
                    Estado = "SP",
                    CEP = "14820-768",
                    Complemento = ""
                },
                new Endereco
                {
                    Id = "148211642500",
                    Rua = "Rua Alan Gustavo Brizolari",
                    Numero = "2500",
                    Bairro = "Jardim Santa Terezinha",
                    Cidade = "Américo Brasiliense",
                    Estado = "SP",
                    CEP = "14821-164",
                    Complemento = "Mansão"
                },
                new Endereco
                {
                    Id = "159970101264",
                    Rua = "Rua Coronel Leão Pio de Freitas",
                    Numero = "1264",
                    Bairro = "Jardim Alvorada",
                    Cidade = "Matão",
                    Estado = "SP",
                    CEP = "15997-010",
                    Complemento = ""
                },
                new Endereco
                {
                    Id = "15997060350",
                    Rua = "Avenida Toledo Malta",
                    Numero = "350",
                    Bairro = "Vila Guarani",
                    Cidade = "Matão",
                    Estado = "SP",
                    CEP = "15997-060",
                    Complemento = ""
                },
                new Endereco
                {
                    Id = "15997088123",
                    Rua = "Avenida Minas Gerais",
                    Numero = "123",
                    Bairro = "Jardim do Bosque",
                    Cidade = "Matão",
                    Estado = "SP",
                    CEP = "15997-088",
                    Complemento = ""
                },
                new Endereco
                {
                    Id = "15997440120",
                    Rua = "Rua Jovelino Constantino",
                    Numero = "120",
                    Bairro = "Jardim Novo Mundo",
                    Cidade = "Matão",
                    Estado = "SP",
                    CEP = "15997-440",
                    Complemento = ""
                },
                new Endereco
                {
                    Id = "15993081140",
                    Rua = "Avenida Baldan",
                    Numero = "140",
                    Bairro = "Residencial Olivio Benassi",
                    Cidade = "Matão",
                    Estado = "SP",
                    CEP = "15993-081",
                    Complemento = ""
                }
            );


            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    CPF = "43625350807",
                    NomeCompleto = "Maria Luiza Guedes",
                    DataNascimento = DateTime.Parse("2003-06-13 00:00:00.0000000"),
                    Email = "maria.luiza@gmail.com",
                    Telefone = "16 992663385",
                    Tipo = "Administrador",
                    EnderecoId = "15997440120",
                    Senha = "malu123",
                    Ativo = true
                },
                new Usuario
                {
                    CPF = "44416401884",
                    NomeCompleto = "Rian Ortega",
                    DataNascimento = DateTime.Parse("2003-05-12 00:00:00.0000000"),
                    Email = "rian.ortega@gmail.com",
                    Telefone = "16 992663385",
                    Tipo = "Agricultor",
                    EnderecoId = "159970101264",
                    Senha = "rian123",
                    Ativo = true
                },
                new Usuario
                {
                    CPF = "47584609831",
                    NomeCompleto = "Amanda Ferreira",
                    DataNascimento = DateTime.Parse("1998-08-17 00:00:00.0000000"),
                    Email = "amanda.ferreira@gmail.com",
                    Telefone = "16 992663385",
                    Tipo = "Administrador",
                    EnderecoId = "14820768140",
                    Senha = "amanda123",
                    Ativo = true
                },
                new Usuario
                {
                    CPF = "51005332851",
                    NomeCompleto = "Danielly Burkowski",
                    DataNascimento = DateTime.Parse("2000-05-10 00:00:00.0000000"),
                    Email = "dani.burk@gmail.com",
                    Telefone = "16 992663385",
                    Tipo = "Agricultor",
                    EnderecoId = "15997060350",
                    Senha = "dani123",
                    Ativo = true
                },
                new Usuario
                {
                    CPF = "53913441824",
                    NomeCompleto = "João Mazzoni",
                    DataNascimento = DateTime.Parse("1998-08-17 00:00:00.0000000"),
                    Email = "joao.mazz@gmail.com",
                    Telefone = "+55 16 91234-5678",
                    Tipo = "Gerente",
                    EnderecoId = "15993081140",
                    Senha = "joao123",
                    Ativo = true
                },
                new Usuario
                {
                    CPF = "86135870033",
                    NomeCompleto = "Carine Souza",
                    DataNascimento = DateTime.Parse("1998-08-21 00:00:00.0000000"),
                    Email = "carine.souza@gmail.com",
                    Telefone = "992663385",
                    Tipo = "Comercial",
                    EnderecoId = "148211642500",
                    Senha = "carine123",
                    Ativo = true
                }
            );

            modelBuilder.Entity<Fornecedor>().HasData(
                new Fornecedor
                {
                    CNPJ = "12.345.678/0001-99",
                    NomeDoFornecedor = "Jardins & Hortas",
                    Telefone = "16 98765-4321",
                    Email = "contato@jardinsehortas.com",
                    TipoDeFornecimento = "Adubo",
                    EnderecoId = "14820768140", 
                    Status = "Ativo"
                },
                new Fornecedor
                {
                    CNPJ = "98.765.432/0001-00",
                    NomeDoFornecedor = "Fazenda Verde",
                    Telefone = "16 99988-7766",
                    Email = "comercial@fazendaverde.com",
                    TipoDeFornecimento = "Agrotoxico",
                    EnderecoId = "148211642500",
                    Status = "Ativo"
                },
                new Fornecedor
                {
                    CNPJ = "11.223.344/0001-55",
                    NomeDoFornecedor = "Pomares",
                    Telefone = "16 99555-3322",
                    Email = "sales@pomares.com",
                    TipoDeFornecimento = "Geral",
                    EnderecoId = "159970101264", 
                    Status = "Inativo"
                },
                new Fornecedor
                {
                    CNPJ = "55.667.788/0001-22",
                    NomeDoFornecedor = "Farmácia Vegetal",
                    Telefone = "16 94444-7777",
                    Email = "info@farmaciavegetal.com",
                    TipoDeFornecimento = "Adubo",
                    EnderecoId = "15997060350", 
                    Status = "Ativo"
                },
                new Fornecedor
                {
                    CNPJ = "22.334.455/0001-88",
                    NomeDoFornecedor = "EcoGarden Supply",
                    Telefone = "16 93333-5555",
                    Email = "contato@ecogardensupply.com",
                    TipoDeFornecimento = "Agrotoxico",
                    EnderecoId = "15997088123", 
                    Status = "Ativo"
                }
            );

            modelBuilder.Entity<Insumo>().HasData(
                new Insumo
                {
                    CodigoLote = Guid.NewGuid().ToString().Substring(0, 8).ToUpper().Replace("-", ""),
                    NomeDoInsumo = "EcoFert Plus",
                    Funcao = "Fertilizante",
                    FornecedorId = "12.345.678/0001-99", 
                    QuantidadeEntrada = 1000,
                    MililitrosAtual = 800,
                    DataValidade = new DateTime(2025, 12, 31),
                    DataEntrada = new DateTime(2024, 11, 22),
                    Status = "Ativo"
                },
                new Insumo
                {
                    CodigoLote = Guid.NewGuid().ToString().Substring(0, 8).ToUpper().Replace("-", ""),
                    NomeDoInsumo = "BioSafe Agro",
                    Funcao = "Agrotoxico",
                    FornecedorId = "98.765.432/0001-00", 
                    QuantidadeEntrada = 500,
                    MililitrosAtual = 450,
                    DataValidade = new DateTime(2026, 08, 15),
                    DataEntrada = new DateTime(2024, 11, 15),
                    Status = "Inativo"
                },
                new Insumo
                {
                    CodigoLote = Guid.NewGuid().ToString().Substring(0, 8).ToUpper().Replace("-", ""),
                    NomeDoInsumo = "AgroNutri Organico",
                    Funcao = "Adubo",
                    FornecedorId = "11.223.344/0001-55", 
                    QuantidadeEntrada = 2000,
                    MililitrosAtual = 1800,
                    DataValidade = new DateTime(2026, 06, 30),
                    DataEntrada = new DateTime(2024, 10, 25),
                    Status = "Ativo"
                },
                new Insumo
                {
                    CodigoLote = Guid.NewGuid().ToString().Substring(0, 8).ToUpper().Replace("-", ""),
                    NomeDoInsumo = "SustainaFert Natural",
                    Funcao = "Fertilizante",
                    FornecedorId = "55.667.788/0001-22", 
                    QuantidadeEntrada = 1500,
                    MililitrosAtual = 1200,
                    DataValidade = new DateTime(2025, 09, 10),
                    DataEntrada = new DateTime(2024, 11, 10),
                    Status = "Ativo"
                },
                new Insumo
                {
                    CodigoLote = Guid.NewGuid().ToString().Substring(0, 8).ToUpper().Replace("-", ""),
                    NomeDoInsumo = "PureSafe Agrotoxico",
                    Funcao = "Agrotoxico",
                    FornecedorId = "22.334.455/0001-88", 
                    QuantidadeEntrada = 800,
                    MililitrosAtual = 750,
                    DataValidade = new DateTime(2027, 01, 20),
                    DataEntrada = new DateTime(2024, 09, 20),
                    Status = "Ativo"
                }
            );

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    CNPJ = "12.345.678/0001-99", 
                    RazaoSocial = "Mercado Verde",
                    Telefone = "(11) 3456-7890",
                    Email = "contato@mercadoverde.com.br",
                    EnderecoId = "14820768140", 
                    Status = "Ativo"
                },
                new Cliente
                {
                    CNPJ = "98.765.432/0001-00", 
                    RazaoSocial = "Hortifruti & Cia",
                    Telefone = "(21) 99876-5432",
                    Email = "contato@hortifrutiecia.com.br",
                    EnderecoId = "148211642500", 
                    Status = "Ativo"
                },
                new Cliente
                {
                    CNPJ = "11.223.344/0001-55", 
                    RazaoSocial = "Varejão da Terra",
                    Telefone = "(31) 3333-5555",
                    Email = "contato@varejaodaterra.com.br",
                    EnderecoId = "159970101264", 
                    Status = "Ativo"
                },
                new Cliente
                {
                    CNPJ = "22.334.455/0001-88", 
                    RazaoSocial = "Supermercado do Campo",
                    Telefone = "(41) 4567-8901",
                    Email = "contato@supermacadocampo.com.br",
                    EnderecoId = "15997060350", 
                    Status = "Ativo"
                },
                new Cliente
                {
                    CNPJ = "24.584.762/0001-12",
                    RazaoSocial = "Jardim das Hortaliças",
                    Telefone = "(41) 4567-8901",
                    Email = "contato@jardimhortalicas.com.br",
                    EnderecoId = "15997060350",
                    Status = "Ativo"
                }
            );
        }
    }
}