﻿// <auto-generated />
using ApiToken;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiToken.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20220925025141_CriandoTabelasFilmes")]
    partial class CriandoTabelasFilmes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiToken.Models.FilmesModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cadastro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Listagem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Voto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("filmesModels");
                });
#pragma warning restore 612, 618
        }
    }
}
