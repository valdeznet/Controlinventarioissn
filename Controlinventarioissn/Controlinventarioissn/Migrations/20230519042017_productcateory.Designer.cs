﻿// <auto-generated />
using System;
using Controlinventarioissn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Controlinventarioissn.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230519042017_productcateory")]
    partial class productcateory
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.Delegacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Delegaciones");
                });

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.Deposito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Depositos");
                });

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.Equipamiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NumeroRfid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Stock")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Equipamientos");
                });

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.EquipamientoCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("EquipamientoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EquipamientoId", "CategoryId")
                        .IsUnique();

                    b.ToTable("EquipamientoCategories");
                });

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.EquipamientoImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EquipamientoId")
                        .HasColumnType("int");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EquipamientoId");

                    b.ToTable("EquipamientoImages");
                });

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DelegacionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DelegacionId");

                    b.HasIndex("Name", "DelegacionId")
                        .IsUnique();

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.EquipamientoCategory", b =>
                {
                    b.HasOne("Controlinventarioissn.Data.Entities.Category", "Category")
                        .WithMany("EquipamientoCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Controlinventarioissn.Data.Entities.Equipamiento", "Equipamiento")
                        .WithMany("EquipamientoCategories")
                        .HasForeignKey("EquipamientoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Equipamiento");
                });

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.EquipamientoImage", b =>
                {
                    b.HasOne("Controlinventarioissn.Data.Entities.Equipamiento", "Equipamiento")
                        .WithMany("EquipamientoImages")
                        .HasForeignKey("EquipamientoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipamiento");
                });

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.Sector", b =>
                {
                    b.HasOne("Controlinventarioissn.Data.Entities.Delegacion", "Delegacion")
                        .WithMany("Sectors")
                        .HasForeignKey("DelegacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Delegacion");
                });

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.Category", b =>
                {
                    b.Navigation("EquipamientoCategories");
                });

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.Delegacion", b =>
                {
                    b.Navigation("Sectors");
                });

            modelBuilder.Entity("Controlinventarioissn.Data.Entities.Equipamiento", b =>
                {
                    b.Navigation("EquipamientoCategories");

                    b.Navigation("EquipamientoImages");
                });
#pragma warning restore 612, 618
        }
    }
}
