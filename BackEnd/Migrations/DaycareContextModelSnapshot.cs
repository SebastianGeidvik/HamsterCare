﻿// <auto-generated />
using System;
using BackEnd;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackEnd.Migrations
{
    [DbContext(typeof(DaycareContext))]
    partial class DaycareContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackEnd.Cage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Cages");
                });

            modelBuilder.Entity("BackEnd.ExerciseCage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("ExerciseCages");
                });

            modelBuilder.Entity("BackEnd.Hamster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("CageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CheckedIn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExerciseCageId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CageId");

                    b.HasIndex("ExerciseCageId");

                    b.ToTable("Hamsters");
                });

            modelBuilder.Entity("BackEnd.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Activity")
                        .HasColumnType("int");

                    b.Property<int?>("HamsterId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HamsterId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("BackEnd.Hamster", b =>
                {
                    b.HasOne("BackEnd.Cage", null)
                        .WithMany("Hamsters")
                        .HasForeignKey("CageId");

                    b.HasOne("BackEnd.ExerciseCage", null)
                        .WithMany("Hamsters")
                        .HasForeignKey("ExerciseCageId");
                });

            modelBuilder.Entity("BackEnd.Log", b =>
                {
                    b.HasOne("BackEnd.Hamster", null)
                        .WithMany("Logs")
                        .HasForeignKey("HamsterId");
                });

            modelBuilder.Entity("BackEnd.Cage", b =>
                {
                    b.Navigation("Hamsters");
                });

            modelBuilder.Entity("BackEnd.ExerciseCage", b =>
                {
                    b.Navigation("Hamsters");
                });

            modelBuilder.Entity("BackEnd.Hamster", b =>
                {
                    b.Navigation("Logs");
                });
#pragma warning restore 612, 618
        }
    }
}
