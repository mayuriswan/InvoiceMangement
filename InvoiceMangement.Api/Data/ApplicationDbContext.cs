using InvoiceMangement.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace InvoiceMangement.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);
                entity.Property(e => e.TotalAmount).HasColumnType("money");
            });

            modelBuilder.Entity<InvoiceDetails>(entity =>
            {
                entity.HasKey(e => e.DetailID);
                entity.Property(e => e.UnitPrice).HasColumnType("money");
                entity.Property(e => e.LineTotal).HasColumnType("money");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryID);
                entity.Property(e => e.CategoryCode)
                      .HasColumnType("nchar(10)")
                      .IsRequired(); // Ensure CategoryCode is required
                entity.Property(e => e.CategoryDescription).HasColumnType("nvarchar(50)");
            });
        }

        public void Initialize()
        {
            var createGetInvoiceSP = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetInvoice')
            BEGIN
                EXEC('
                    CREATE PROCEDURE [dbo].[GetInvoice]
                        @InvoiceNumber NVARCHAR(50), 
                        @InvoiceDate DATE
                    AS
                    BEGIN
                        SELECT
                            Invoice.InvoiceID,
                            Invoice.InvoiceNumber,
                            Invoice.InvoiceDate,
                            Invoice.CustomerID,
                            Invoice.TotalAmount,
                            (SELECT
                                DetailID,
                                ProductID,
                                Quantity,
                                UnitPrice,
                                LineTotal
                             FROM InvoiceDetails
                             WHERE InvoiceDetails.InvoiceID = Invoice.InvoiceID
                             FOR JSON PATH) AS InvoiceDetails
                        FROM Invoice
                        WHERE Invoice.InvoiceNumber = @InvoiceNumber
                        FOR JSON PATH, ROOT(''Invoice'');
                    END
                ')
            END";

            Database.ExecuteSqlRaw(createGetInvoiceSP);
        }

        public void Seed()
        {
            // Ensure the database is created
            Database.EnsureCreated();

            // Check if the Categories are already seeded
            if (!Categories.Any())
            {
                // Predefined categories to be inserted
                var categories = new[]
                {
                    new Category { CategoryCode = "Servers   ", CategoryDescription = "Servers" },
                    new Category { CategoryCode = "Software  ", CategoryDescription = "Software" },
                    new Category { CategoryCode = "PC        ", CategoryDescription = "Desktops" },
                    new Category { CategoryCode = "Laptops   ", CategoryDescription = "Laptops" }
                };

                // Validate that no category code is null
                foreach (var category in categories)
                {
                    if (string.IsNullOrWhiteSpace(category.CategoryCode))
                    {
                        throw new InvalidOperationException("CategoryCode cannot be null or empty");
                    }
                }

                // Add categories to the context
                Categories.AddRange(categories);
                SaveChanges();  // Save changes to ensure CategoryIDs are generated
            }

            // Fetch the CategoryIDs for the predefined categories
            var serversCategory = Categories.First(c => c.CategoryCode == "Servers   ").CategoryID;
            var softwareCategory = Categories.First(c => c.CategoryCode == "Software  ").CategoryID;
            var desktopsCategory = Categories.First(c => c.CategoryCode == "PC        ").CategoryID;
            var laptopsCategory = Categories.First(c => c.CategoryCode == "Laptops   ").CategoryID;

            // Check if there are no invoices and invoice details already seeded
            if (!Invoices.Any() && !InvoiceDetails.Any())
            {
                // Creating invoices associated with the seeded categories
                var invoices = new[]
                {
                    new Invoice
                    {
                        InvoiceNumber = "INV-001",
                        InvoiceDate = DateTime.Parse("2024-05-16"),
                        CategoryID = serversCategory, // References 'Servers'
                        CustomerID = "456",
                        TotalAmount = 1500,
                        PaymentDueDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                    },
                    new Invoice
                    {
                        InvoiceNumber = "40001",
                        InvoiceDate = DateTime.Parse("2024-05-16"),
                        CategoryID = softwareCategory, // References 'Software'
                        CustomerID = "IBM",
                        TotalAmount = 5000,
                        PaymentDueDate = DateTime.Parse("2024-06-15"),
                        CreatedDate = DateTime.Parse("2024-05-16")
                    },
                    new Invoice
                    {
                        InvoiceNumber = "40002",
                        InvoiceDate = DateTime.Parse("2024-05-16"),
                        CategoryID = laptopsCategory, // References 'Laptops'
                        CustomerID = "Microsoft",
                        TotalAmount = 4000,
                        PaymentDueDate = DateTime.Parse("2024-06-16"),
                        CreatedDate = DateTime.Parse("2024-05-16")
                    }
                };

                Invoices.AddRange(invoices);
                SaveChanges();  // Save the invoices to generate InvoiceIDs

                // Creating invoice details for the first invoice as an example
                var invoiceDetails = new[]
                {
                    new InvoiceDetails
                    {
                        InvoiceID = invoices[0].InvoiceID, // Automatically linked to the first invoice
                        ProductID = "789",
                        UnitPrice = 500.00M,
                        Quantity = 2,
                        LineTotal = 1000.00M
                    },
                    new InvoiceDetails
                    {
                        InvoiceID = invoices[1].InvoiceID, // Automatically linked to the second invoice
                        ProductID = "987",
                        UnitPrice = 500.00M,
                        Quantity = 1,
                        LineTotal = 500.00M
                    }
                };

                InvoiceDetails.AddRange(invoiceDetails);
                SaveChanges(); // Finally save the invoice details
            }
        }
    }
}
