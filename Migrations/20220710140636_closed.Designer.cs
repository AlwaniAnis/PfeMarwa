﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tracerapi.Data;

#nullable disable

namespace tracerapi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220710140636_closed")]
    partial class closed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("tracerapi.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("tracerapi.Models.Incident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AskToClose")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateIncident")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("File")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumContratMaintenance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumSerie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priorite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Statut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sujet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypePrestation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("closed")
                        .HasColumnType("bit");

                    b.Property<decimal>("duration")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("tracerapi.Models.Intervention", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CodeContrat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Compte")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContratMaintenance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateAppel")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateContrat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEcheance")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateIntervention")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateProchaineEcheance")
                        .HasColumnType("datetime2");

                    b.Property<string>("File")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sujet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Interventions");
                });

            modelBuilder.Entity("tracerapi.Models.Tache", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CodeContrat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Compte")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContratMaintenance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateAppel")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateContrat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEcheance")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateProchaineEcheance")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTache")
                        .HasColumnType("datetime2");

                    b.Property<string>("File")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sujet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("closed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Taches");
                });
#pragma warning restore 612, 618
        }
    }
}
