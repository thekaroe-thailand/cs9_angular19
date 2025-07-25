﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApiApp;

#nullable disable

namespace WebApiApp.Migrations
{
    [DbContext(typeof(BookContext))]
    [Migration("20250629154540_CreateTableBillSaleAndBillSaleDetail")]
    partial class CreateTableBillSaleAndBillSaleDetail
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApiApp.Models.BillSaleDetailModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BillSaleId")
                        .HasColumnType("integer");

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("Qty")
                        .HasColumnType("integer");

                    b.Property<int>("Total")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BillSaleId");

                    b.HasIndex("BookId");

                    b.ToTable("BillSaleDetailModel");
                });

            modelBuilder.Entity("WebApiApp.Models.BillSaleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BillSaleModel");
                });

            modelBuilder.Entity("WebApiApp.Models.BookModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Isbn")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer");

                    b.Property<int?>("StockId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.HasIndex("StockId");

                    b.ToTable("BookModel");
                });

            modelBuilder.Entity("WebApiApp.Models.ModelPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ModelPerson");
                });

            modelBuilder.Entity("WebApiApp.Models.PublisherModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PublisherModel");
                });

            modelBuilder.Entity("WebApiApp.Models.SaleTempModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("Qty")
                        .HasColumnType("integer");

                    b.Property<int>("Total")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SaleTempModel");
                });

            modelBuilder.Entity("WebApiApp.Models.StockModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("Remark")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("StockModel");
                });

            modelBuilder.Entity("WebApiApp.Models.BillSaleDetailModel", b =>
                {
                    b.HasOne("WebApiApp.Models.BillSaleModel", "BillSale")
                        .WithMany("BillSaleDetails")
                        .HasForeignKey("BillSaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiApp.Models.BookModel", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BillSale");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("WebApiApp.Models.BookModel", b =>
                {
                    b.HasOne("WebApiApp.Models.PublisherModel", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiApp.Models.StockModel", "Stock")
                        .WithMany("Books")
                        .HasForeignKey("StockId");

                    b.Navigation("Publisher");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("WebApiApp.Models.StockModel", b =>
                {
                    b.HasOne("WebApiApp.Models.BookModel", "Book")
                        .WithMany("Stocks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("WebApiApp.Models.BillSaleModel", b =>
                {
                    b.Navigation("BillSaleDetails");
                });

            modelBuilder.Entity("WebApiApp.Models.BookModel", b =>
                {
                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("WebApiApp.Models.PublisherModel", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("WebApiApp.Models.StockModel", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
