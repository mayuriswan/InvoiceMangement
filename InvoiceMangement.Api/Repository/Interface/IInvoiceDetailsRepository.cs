using InvoiceMangement.Api.Models;

namespace InvoiceMangement.Api.Repository.Interface
{
    public interface IInvoiceDetailsRepository
    {
        Task<IEnumerable<InvoiceDetails>> GetByInvoiceIdAsync(int invoiceId);
        Task<InvoiceDetails> GetByIdAsync(int id);
        Task AddAsync(InvoiceDetails invoiceDetails);
        Task UpdateAsync(InvoiceDetails invoiceDetails);
        Task DeleteAsync(int id);
    }
}
