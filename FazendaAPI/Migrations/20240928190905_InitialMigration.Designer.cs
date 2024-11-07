﻿// <auto-generated />
using System;
using FazendaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FazendaAPI.Migrations
{
    [DbContext(typeof(FazendaAPIContext))]
    [Migration("20240928190905_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Fornecedor", b =>
                {
                    b.Property<string>("CNPJ")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnderecoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NomeDoFornecedor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDeFornecimento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CNPJ");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Fornecedor");
                });

            modelBuilder.Entity("Insumo", b =>
                {
                    b.Property<string>("CodigoLote")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("datetime2");

                    b.Property<string>("FornecedorCNPJ")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Funcao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MililitrosAtual")
                        .HasColumnType("int");

                    b.Property<string>("NomeDoInsumo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantidadeEntrada")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CodigoLote");

                    b.HasIndex("FornecedorCNPJ");

                    b.ToTable("Insumo");
                });

            modelBuilder.Entity("Models.AplicacaoInsumo", b =>
                {
                    b.Property<int>("Registro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Registro"), 1L, 1);

                    b.Property<DateTime>("DataAplicacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsumoCodigoLote")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PlantacaoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Registro");

                    b.HasIndex("InsumoCodigoLote");

                    b.HasIndex("PlantacaoId");

                    b.ToTable("AplicacaoInsumo");
                });

            modelBuilder.Entity("Models.Cliente", b =>
                {
                    b.Property<string>("CNPJ")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnderecoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CNPJ");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Models.Colheita", b =>
                {
                    b.Property<int>("NumeroRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NumeroRegistro"), 1L, 1);

                    b.Property<DateTime>("DataColheita")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlantacaoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("QuantidadeColhida")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NumeroRegistro");

                    b.HasIndex("PlantacaoId");

                    b.ToTable("Colheita");
                });

            modelBuilder.Entity("Models.Endereco", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("Models.RegistroInfestacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cauterizado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataConclusaoTratamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlantacaoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PragaId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlantacaoId");

                    b.HasIndex("PragaId");

                    b.ToTable("RegistroInfestacao");
                });

            modelBuilder.Entity("Models.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativa")
                        .HasColumnType("bit");

                    b.Property<string>("ClienteCNPJ")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DataDaVenda")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormaPagamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FuncionarioCPF")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteCNPJ");

                    b.HasIndex("FuncionarioCPF");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("Plantacao", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Colheita")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CondicaoClimatica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Crescimento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataDePlantio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Irrigacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalDePlantio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LuzSolar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Plantacao");
                });

            modelBuilder.Entity("Praga", b =>
                {
                    b.Property<string>("PragaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ControleAdequado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeDaPraga")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoControle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoPraga")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PragaId");

                    b.ToTable("Praga");

                    b.HasData(
                        new
                        {
                            PragaId = "001",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Pulgões",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "002",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Lagartas",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "003",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Cochonilhas",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "004",
                            ControleAdequado = "Barreiras físicas, remoção manual",
                            NomeDaPraga = "Lesmas",
                            TipoControle = "Cultural",
                            TipoPraga = "Molusco"
                        },
                        new
                        {
                            PragaId = "005",
                            ControleAdequado = "Barreiras físicas, remoção manual",
                            NomeDaPraga = "Caracóis",
                            TipoControle = "Cultural",
                            TipoPraga = "Molusco"
                        },
                        new
                        {
                            PragaId = "006",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Moscas",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "007",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Mosquitos",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "008",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Trips",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "009",
                            ControleAdequado = "Ácaros predadores",
                            NomeDaPraga = "Ácaros",
                            TipoControle = "Biológico",
                            TipoPraga = "Aracnídeo"
                        },
                        new
                        {
                            PragaId = "010",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Besouros",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "011",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Formigas",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "012",
                            ControleAdequado = "Rotação de culturas, solarização do solo",
                            NomeDaPraga = "Nematóides",
                            TipoControle = "Cultural",
                            TipoPraga = "Verme"
                        },
                        new
                        {
                            PragaId = "013",
                            ControleAdequado = "Rotação de culturas, remoção de plantas infestadas",
                            NomeDaPraga = "Cigarras",
                            TipoControle = "Cultural",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "014",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Brocas",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "015",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Percevejos",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "016",
                            ControleAdequado = "Manejo adequado do solo, cobertura do solo",
                            NomeDaPraga = "Grilos",
                            TipoControle = "Cultural",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "017",
                            ControleAdequado = "Rotação de culturas, manutenção de áreas limpas",
                            NomeDaPraga = "Gafanhotos",
                            TipoControle = "Cultural",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "018",
                            ControleAdequado = "Plantio em sistemas que dificultem o acesso, uso de barreiras",
                            NomeDaPraga = "Minhocas Cortadeiras",
                            TipoControle = "Cultural",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "019",
                            ControleAdequado = "Manutenção adequada da estrutura de madeira, remoção de madeiras deterioradas",
                            NomeDaPraga = "Bicho Carpinteiro",
                            TipoControle = "Cultural",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "020",
                            ControleAdequado = "Rotação de culturas, uso de solo saudável",
                            NomeDaPraga = "Vermes",
                            TipoControle = "Cultural",
                            TipoPraga = "Verme"
                        },
                        new
                        {
                            PragaId = "021",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Psilídeos",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "022",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Cicadelídeos",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "023",
                            ControleAdequado = "Manutenção de áreas limpas, uso de armadilhas",
                            NomeDaPraga = "Bicho da Luz",
                            TipoControle = "Cultural",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "024",
                            ControleAdequado = "Uso de predadores naturais",
                            NomeDaPraga = "Aranhas",
                            TipoControle = "Biológico",
                            TipoPraga = "Aracnídeo"
                        },
                        new
                        {
                            PragaId = "025",
                            ControleAdequado = "Inseticida",
                            NomeDaPraga = "Gorgulhos",
                            TipoControle = "Químico",
                            TipoPraga = "Inseto"
                        },
                        new
                        {
                            PragaId = "026",
                            ControleAdequado = "Fungicida",
                            NomeDaPraga = "Oídio",
                            TipoControle = "Químico",
                            TipoPraga = "Fungo"
                        },
                        new
                        {
                            PragaId = "027",
                            ControleAdequado = "Fungicida",
                            NomeDaPraga = "Ferrugem",
                            TipoControle = "Químico",
                            TipoPraga = "Fungo"
                        },
                        new
                        {
                            PragaId = "028",
                            ControleAdequado = "Fungicida",
                            NomeDaPraga = "Míldio",
                            TipoControle = "Químico",
                            TipoPraga = "Fungo"
                        },
                        new
                        {
                            PragaId = "029",
                            ControleAdequado = "Fungicida",
                            NomeDaPraga = "Antracnose",
                            TipoControle = "Químico",
                            TipoPraga = "Fungo"
                        },
                        new
                        {
                            PragaId = "030",
                            ControleAdequado = "Fungicida",
                            NomeDaPraga = "Fusarinose",
                            TipoControle = "Químico",
                            TipoPraga = "Fungo"
                        },
                        new
                        {
                            PragaId = "031",
                            ControleAdequado = "Fungicida",
                            NomeDaPraga = "Mofo",
                            TipoControle = "Químico",
                            TipoPraga = "Fungo"
                        },
                        new
                        {
                            PragaId = "032",
                            ControleAdequado = "Fungicida",
                            NomeDaPraga = "Cytospora",
                            TipoControle = "Químico",
                            TipoPraga = "Fungo"
                        },
                        new
                        {
                            PragaId = "033",
                            ControleAdequado = "Fungicida",
                            NomeDaPraga = "Mancha Foliar",
                            TipoControle = "Químico",
                            TipoPraga = "Fungo"
                        },
                        new
                        {
                            PragaId = "034",
                            ControleAdequado = "Fungicida",
                            NomeDaPraga = "Podridão",
                            TipoControle = "Químico",
                            TipoPraga = "Fungo"
                        });
                });

            modelBuilder.Entity("Produto", b =>
                {
                    b.Property<string>("Lote")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoriaProduto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ColheitaOrigemNumeroRegistro")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomeProduto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Quantidade")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Lote");

                    b.HasIndex("ColheitaOrigemNumeroRegistro");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("ProdutoVenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ProdutoLote")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("Quantidade")
                        .HasColumnType("bigint");

                    b.Property<int>("VendaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoLote");

                    b.HasIndex("VendaId");

                    b.ToTable("ProdutoVenda");
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnderecoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CPF");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Fornecedor", b =>
                {
                    b.HasOne("Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Insumo", b =>
                {
                    b.HasOne("Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("FornecedorCNPJ");

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("Models.AplicacaoInsumo", b =>
                {
                    b.HasOne("Insumo", "Insumo")
                        .WithMany()
                        .HasForeignKey("InsumoCodigoLote")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Plantacao", "Plantacao")
                        .WithMany()
                        .HasForeignKey("PlantacaoId");

                    b.Navigation("Insumo");

                    b.Navigation("Plantacao");
                });

            modelBuilder.Entity("Models.Cliente", b =>
                {
                    b.HasOne("Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Models.Colheita", b =>
                {
                    b.HasOne("Plantacao", "Plantacao")
                        .WithMany()
                        .HasForeignKey("PlantacaoId");

                    b.Navigation("Plantacao");
                });

            modelBuilder.Entity("Models.RegistroInfestacao", b =>
                {
                    b.HasOne("Plantacao", "Plantacao")
                        .WithMany()
                        .HasForeignKey("PlantacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Praga", "Praga")
                        .WithMany()
                        .HasForeignKey("PragaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plantacao");

                    b.Navigation("Praga");
                });

            modelBuilder.Entity("Models.Venda", b =>
                {
                    b.HasOne("Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteCNPJ");

                    b.HasOne("Usuario", "Funcionario")
                        .WithMany()
                        .HasForeignKey("FuncionarioCPF");

                    b.Navigation("Cliente");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("Produto", b =>
                {
                    b.HasOne("Models.Colheita", "ColheitaOrigem")
                        .WithMany()
                        .HasForeignKey("ColheitaOrigemNumeroRegistro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ColheitaOrigem");
                });

            modelBuilder.Entity("ProdutoVenda", b =>
                {
                    b.HasOne("Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoLote");

                    b.HasOne("Models.Venda", null)
                        .WithMany("Produtos")
                        .HasForeignKey("VendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.HasOne("Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Models.Venda", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}