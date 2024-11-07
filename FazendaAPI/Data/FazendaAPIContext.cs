
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
                // IDs formatados manualmente com três dígitos
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

        }
    }
}