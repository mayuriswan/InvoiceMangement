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
            var invoices = await _context.Invoices
                .Include(i=>i.Category).Include(i => i.InvoiceDetails).ToListAsync();
            return invoices; 
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _context.Invoices.Include(i=>i.Category).Include(i => i.InvoiceDetails).FirstOrDefaultAsync(i => i.InvoiceID == id);
        }

        public async Task AddAsync(Invoice invoice)
        {
            invoice.Category = null;
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            var existingInvoice = await _context.Invoices
                                                 .Include(i => i.Category)
                                                 .Include(i => i.InvoiceDetails)
                                                 .FirstOrDefaultAsync(i => i.InvoiceID == invoice.InvoiceID);

            if (existingInvoice == null)
            {
                throw new Exception("Invoice not found");
            }

            // Update the invoice fields
            _context.Entry(existingInvoice).CurrentValues.SetValues(invoice);

            // Update the category if it has changed
            if (existingInvoice.CategoryID != invoice.CategoryID)
            {
                existingInvoice.CategoryID = invoice.CategoryID;
                var newCategory = await _context.Categories.FindAsync(invoice.CategoryID);
                if (newCategory != null)
                {
                    existingInvoice.Category = newCategory;
                }
            }

            // Handle line items
            foreach (var existingDetail in existingInvoice.InvoiceDetails.ToList())
            {
                if (!invoice.InvoiceDetails.Any(d => d.DetailID == existingDetail.DetailID))
                {
                    _context.InvoiceDetails.Remove(existingDetail);
                }
            }

            foreach (var detail in invoice.InvoiceDetails)
            {
                var existingDetail = existingInvoice.InvoiceDetails
                                                    .FirstOrDefault(d => d.DetailID == detail.DetailID);

                if (existingDetail != null)
                {
                    _context.Entry(existingDetail).CurrentValues.SetValues(detail);
                }
                else
                {
                    existingInvoice.InvoiceDetails.Add(detail);
                }
            }

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
