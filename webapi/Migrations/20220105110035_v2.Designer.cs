﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Backend.Migrations
{
    [DbContext(typeof(EvidencijaContext))]
    [Migration("20220105110035_v2")]
    partial class v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Drzava", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Drzava");
                });

            modelBuilder.Entity("Models.Igrac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlitzRating")
                        .HasColumnType("int");

                    b.Property<int>("ClassicalRating")
                        .HasColumnType("int");

                    b.Property<int?>("DrzavaID")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("RapidRating")
                        .HasColumnType("int");

                    b.Property<int?>("TitulaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DrzavaID");

                    b.HasIndex("TitulaID");

                    b.ToTable("Igraci");
                });

            modelBuilder.Entity("Models.Partija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BeliID")
                        .HasColumnType("int");

                    b.Property<int>("BrojPoteza")
                        .HasColumnType("int");

                    b.Property<int?>("CrniID")
                        .HasColumnType("int");

                    b.Property<string>("Ishod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notacija")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("Runda")
                        .HasColumnType("int");

                    b.Property<int?>("TurnirID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BeliID");

                    b.HasIndex("CrniID");

                    b.HasIndex("TurnirID");

                    b.ToTable("Partije");
                });

            modelBuilder.Entity("Models.Titula", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ID");

                    b.ToTable("Titula");
                });

            modelBuilder.Entity("Models.Turnir", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DatumDo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatumOd")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DrzavaID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("DrzavaID");

                    b.ToTable("Turniri");
                });

            modelBuilder.Entity("Models.Ucesnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Bodovi")
                        .HasColumnType("int");

                    b.Property<int?>("IgracID")
                        .HasColumnType("int");

                    b.Property<int>("Mesto")
                        .HasColumnType("int");

                    b.Property<int?>("TurnirID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IgracID");

                    b.HasIndex("TurnirID");

                    b.ToTable("Ucesnici");
                });

            modelBuilder.Entity("Models.Igrac", b =>
                {
                    b.HasOne("Models.Drzava", "Drzava")
                        .WithMany("Predstavnici")
                        .HasForeignKey("DrzavaID");

                    b.HasOne("Models.Titula", "Titula")
                        .WithMany("Igraci")
                        .HasForeignKey("TitulaID");

                    b.Navigation("Drzava");

                    b.Navigation("Titula");
                });

            modelBuilder.Entity("Models.Partija", b =>
                {
                    b.HasOne("Models.Igrac", "Beli")
                        .WithMany("PBeli")
                        .HasForeignKey("BeliID");

                    b.HasOne("Models.Igrac", "Crni")
                        .WithMany("PCrni")
                        .HasForeignKey("CrniID");

                    b.HasOne("Models.Turnir", null)
                        .WithMany("Partije")
                        .HasForeignKey("TurnirID");

                    b.Navigation("Beli");

                    b.Navigation("Crni");
                });

            modelBuilder.Entity("Models.Turnir", b =>
                {
                    b.HasOne("Models.Drzava", "Drzava")
                        .WithMany("TurniriLokacije")
                        .HasForeignKey("DrzavaID");

                    b.Navigation("Drzava");
                });

            modelBuilder.Entity("Models.Ucesnik", b =>
                {
                    b.HasOne("Models.Igrac", "Igrac")
                        .WithMany("Ucesnik")
                        .HasForeignKey("IgracID");

                    b.HasOne("Models.Turnir", "Turnir")
                        .WithMany("Ucesnici")
                        .HasForeignKey("TurnirID");

                    b.Navigation("Igrac");

                    b.Navigation("Turnir");
                });

            modelBuilder.Entity("Models.Drzava", b =>
                {
                    b.Navigation("Predstavnici");

                    b.Navigation("TurniriLokacije");
                });

            modelBuilder.Entity("Models.Igrac", b =>
                {
                    b.Navigation("PBeli");

                    b.Navigation("PCrni");

                    b.Navigation("Ucesnik");
                });

            modelBuilder.Entity("Models.Titula", b =>
                {
                    b.Navigation("Igraci");
                });

            modelBuilder.Entity("Models.Turnir", b =>
                {
                    b.Navigation("Partije");

                    b.Navigation("Ucesnici");
                });
#pragma warning restore 612, 618
        }
    }
}
