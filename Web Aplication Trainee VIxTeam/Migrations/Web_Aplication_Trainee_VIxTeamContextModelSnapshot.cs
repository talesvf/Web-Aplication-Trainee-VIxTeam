﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web_Aplication_Trainee_VIxTeam.Data;

#nullable disable

namespace Web_Aplication_Trainee_VIxTeam.Migrations
{
    [DbContext(typeof(Web_Aplication_Trainee_VIxTeamContext))]
    partial class Web_Aplication_Trainee_VIxTeamContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Web_Aplication_Trainee_VIxTeam.Models.PessoaModel", b =>
                {
                    b.Property<int>("CodigoPessoa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoPessoa"), 1L, 1);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomePessoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QtdFilhos")
                        .HasColumnType("int");

                    b.Property<decimal>("Salario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Situacao")
                        .HasColumnType("bit");

                    b.HasKey("CodigoPessoa");

                    b.ToTable("PessoaModel");
                });
#pragma warning restore 612, 618
        }
    }
}