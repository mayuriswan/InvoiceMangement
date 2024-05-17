using InvoiceMangement.Api.Data;
using InvoiceMangement.Api.Models;
using InvoiceMangement.Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace InvoiceMangement.Api.Repository.Implementation
{
    public class InvoiceLineItemRepository : IInvoiceLineItemRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceLineItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InvoiceLineItem>> GetByInvoiceHeaderIdAsync(int invoiceHeaderId)
        {
            return await _context.InvoiceLineItems.Where(i => i.InvoiceHeaderId == invoiceHeaderId).ToListAsync();
        }

        public async Task<InvoiceLineItem> GetByIdAsync(int id)
        {
            return await _context.InvoiceLineItems.FindAsync(id);
        }

        public async Task AddAsync(InvoiceLineItem invoiceLineItem)
        {
            _context.InvoiceLineItems.Add(invoiceLineItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(InvoiceLineItem invoiceLineItem)
        {
            _context.Entry(invoiceLineItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var invoiceLineItem = await _context.InvoiceLineItems.FindAsync(id);
            _context.InvoiceLineItems.Remove(invoiceLineItem);
            await _context.SaveChangesAsync();
        }
    }
}