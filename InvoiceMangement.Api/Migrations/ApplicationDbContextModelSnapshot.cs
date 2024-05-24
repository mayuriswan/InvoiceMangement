﻿// <auto-generated />
using System;
using InvoiceMangement.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InvoiceMangement.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InvoiceMangement.Api.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryCode")
                        .IsRequired()
                        .HasColumnType("nchar(10)");

                    b.Property<string>("CategoryDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("InvoiceMangement.Api.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PaymentDueDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("money");

                    b.HasKey("InvoiceID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("InvoiceMangement.Api.Models.InvoiceDetails", b =>
                {
                    b.Property<int>("DetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetailID"));

                    b.Property<int>("InvoiceID")
                        .HasColumnType("int");

                    b.Property<decimal>("LineTotal")
                        .HasColumnType("money");

                    b.Property<string>("ProductID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.HasKey("DetailID");

                    b.HasIndex("InvoiceID");

                    b.ToTable("InvoiceDetails");
                });

            modelBuilder.Entity("InvoiceMangement.Api.Models.Invoice", b =>
                {
                    b.HasOne("InvoiceMangement.Api.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("InvoiceMangement.Api.Models.InvoiceDetails", b =>
                {
                    b.HasOne("InvoiceMangement.Api.Models.Invoice", null)
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("InvoiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InvoiceMangement.Api.Models.Invoice", b =>
                {
                    b.Navigation("InvoiceDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
