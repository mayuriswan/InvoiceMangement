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

        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InvoiceHeader>()
                .HasMany(h => h.InvoiceLineItems)
                .WithOne(i => i.InvoiceHeader)
                .HasForeignKey(i => i.InvoiceHeaderId);
        }
    }
}
