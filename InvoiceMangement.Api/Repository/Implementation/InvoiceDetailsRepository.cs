using InvoiceMangement.Api.Data;
using InvoiceMangement.Api.Models;
using InvoiceMangement.Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace InvoiceMangement.Api.Repository.Implementation
{
    public class InvoiceDetailsRepository : IInvoiceDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InvoiceDetails>> GetByInvoiceIdAsync(int invoiceId)
        {
            return await _context.InvoiceDetails.Where(i => i.InvoiceID == invoiceId).ToListAsync();
        }

        public async Task<InvoiceDetails> GetByIdAsync(int id)
        {
            return await _context.InvoiceDetails.FindAsync(id);
        }

        public async Task AddAsync(InvoiceDetails invoiceDetails)
        {
            _context.InvoiceDetails.Add(invoiceDetails);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(InvoiceDetails invoiceDetails)
        {
            _context.Entry(invoiceDetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var invoiceDetails = await _context.InvoiceDetails.FindAsync(id);
            _context.InvoiceDetails.Remove(invoiceDetails);
            await _context.SaveChangesAsync();
        }
    }
}