using InvoiceMangement.Api.Models;
using Microsoft.EntityFrameworkCore;

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
            if (!Invoices.Any() && !InvoiceDetails.Any())
            {
                var invoices = new[]
                {
                    new Invoice
                    {
                        InvoiceNumber = "INV-001",
                        InvoiceDate = DateTime.Now, // Assuming null as the date is missing
                        CustomerID = "456",
                        TotalAmount = 1500,
                        PaymentDueDate = DateTime.Now, // Assuming null as the date is missing
                        CreatedDate = DateTime.Now, // Assuming null as the date is missing
                    },
                    new Invoice
                    {
                        InvoiceNumber = "40001",
                        InvoiceDate = DateTime.Parse("2024-05-16"),
                        CustomerID = "IBM",
                        TotalAmount = 5000,
                        PaymentDueDate = DateTime.Parse("2024-06-15"),
                        CreatedDate = DateTime.Parse("2024-05-16")
                    },
                    new Invoice
                    {
                        InvoiceNumber = "40002",
                        InvoiceDate = DateTime.Parse("2024-05-16"),
                        CustomerID = "Microsfot",
                        TotalAmount = 4000,
                        PaymentDueDate = DateTime.Parse("2024-06-16"),
                        CreatedDate = DateTime.Parse("2024-05-16")
                    }
                };

                var invoiceDetails = new[]
                {
                    new InvoiceDetails
                    {
                        InvoiceID = 10,
                        ProductID = "789",
                        UnitPrice = 500.00M,
                        Quantity = 2,
                        LineTotal = 1000.00M
                    },
                    new InvoiceDetails
                    {
                        InvoiceID = 10,
                        ProductID = "987",
                        UnitPrice = 500.00M,
                        Quantity = 1,
                        LineTotal = 500.00M
                    }
                };

                Invoices.AddRange(invoices);
                InvoiceDetails.AddRange(invoiceDetails);

                SaveChanges();
            }
        }
        
    }
}
