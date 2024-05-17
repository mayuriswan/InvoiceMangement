namespace InvoiceMangement.Api.Repository.Implementation
{
    using InvoiceMangement.Api.Data;
    using InvoiceMangement.Api.Models;
    using InvoiceMangement.Api.Repository.Interface;
    // Repositories/InvoiceHeaderRepository.cs
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class InvoiceHeaderRepository : IInvoiceHeaderRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceHeaderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InvoiceHeader>> GetAllAsync()
        {
            return await _context.InvoiceHeaders.ToListAsync();
        }

        public async Task<InvoiceHeader> GetByIdAsync(int id)
        {
            return await _context.InvoiceHeaders.Include(h => h.InvoiceLineItems).FirstOrDefaultAsync(h => h.InvoiceHeaderId == id);
        }

        public async Task AddAsync(InvoiceHeader invoiceHeader)
        {
            _context.InvoiceHeaders.Add(invoiceHeader);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(InvoiceHeader invoiceHeader)
        {
            _context.Entry(invoiceHeader).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var invoiceHeader = await _context.InvoiceHeaders.FindAsync(id);
            _context.InvoiceHeaders.Remove(invoiceHeader);
            await _context.SaveChangesAsync();
        }
    }

}
