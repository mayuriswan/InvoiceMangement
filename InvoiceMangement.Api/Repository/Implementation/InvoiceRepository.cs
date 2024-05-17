namespace InvoiceMangement.Api.Repository.Implementation
{
    using InvoiceMangement.Api.Data;
    using InvoiceMangement.Api.Models;
    using InvoiceMangement.Api.Repository.Interface;
    // Repositories/InvoiceHeaderRepository.cs
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices.Include(i => i.InvoiceDetails).ToListAsync();
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _context.Invoices.Include(i => i.InvoiceDetails).FirstOrDefaultAsync(i => i.InvoiceID == id);
        }

        public async Task AddAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            _context.Entry(invoice).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
