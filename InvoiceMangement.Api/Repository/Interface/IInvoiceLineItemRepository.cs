using InvoiceMangement.Api.Models;

namespace InvoiceMangement.Api.Repository.Interface
{
    public interface IInvoiceLineItemRepository
    {
        Task<IEnumerable<InvoiceLineItem>> GetByInvoiceHeaderIdAsync(int invoiceHeaderId);
        Task<InvoiceLineItem> GetByIdAsync(int id);
        Task AddAsync(InvoiceLineItem invoiceLineItem);
        Task UpdateAsync(InvoiceLineItem invoiceLineItem);
        Task DeleteAsync(int id);
    }
}
