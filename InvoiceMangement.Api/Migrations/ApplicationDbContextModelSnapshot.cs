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

            modelBuilder.Entity("InvoiceMangement.Api.Models.InvoiceHeader", b =>
                {
                    b.Property<int>("InvoiceHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceHeaderId"));

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceHeaderId");

                    b.ToTable("InvoiceHeaders");
                });

            modelBuilder.Entity("InvoiceMangement.Api.Models.InvoiceLineItem", b =>
                {
                    b.Property<int>("InvoiceLineItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceLineItemId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InvoiceHeaderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceLineItemId");

                    b.HasIndex("InvoiceHeaderId");

                    b.ToTable("InvoiceLineItems");
                });

            modelBuilder.Entity("InvoiceMangement.Api.Models.InvoiceLineItem", b =>
                {
                    b.HasOne("InvoiceMangement.Api.Models.InvoiceHeader", "InvoiceHeader")
                        .WithMany("InvoiceLineItems")
                        .HasForeignKey("InvoiceHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InvoiceHeader");
                });

            modelBuilder.Entity("InvoiceMangement.Api.Models.InvoiceHeader", b =>
                {
                    b.Navigation("InvoiceLineItems");
                });
#pragma warning restore 612, 618
        }
    }
}
