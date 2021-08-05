﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SGE.Context;

namespace SGE.Context.Migrations
{
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(SGEDbContext))]
    partial class SGEDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SGE.Context.Models.Ponto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("Data")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DataCriacao")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DataModificacao")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Tarefa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Ponto");
                });

            modelBuilder.Entity("SGE.Context.Models.Requerimento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("DataCriacao")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DataModificacao")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Justificativa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PontoId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PontoId")
                        .IsUnique();

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Requerimento");
                });

            modelBuilder.Entity("SGE.Context.Models.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DataCriacao")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DataModificacao")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Ativo = true,
                            CPF = "00000000000",
                            DataCriacao = new DateTimeOffset(new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)),
                            Login = "admin@sge.com",
                            Nome = "Usuário Administrador SGE",
                            Perfil = 1,
                            Senha = "$2a$11$llgzqoCJoRHxX00w3rqdwO8yN1/xvq1w.UERLsN4KjTFo/2Dvk3mS"
                        });
                });

            modelBuilder.Entity("SGE.Context.Models.Ponto", b =>
                {
                    b.HasOne("SGE.Context.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SGE.Context.Models.Requerimento", b =>
                {
                    b.HasOne("SGE.Context.Models.Ponto", "Ponto")
                        .WithOne()
                        .HasForeignKey("SGE.Context.Models.Requerimento", "PontoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SGE.Context.Models.Usuario", "Usuario")
                        .WithOne()
                        .HasForeignKey("SGE.Context.Models.Requerimento", "UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
