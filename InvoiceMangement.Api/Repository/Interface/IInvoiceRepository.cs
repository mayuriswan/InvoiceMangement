using InvoiceMangement.Api.Models;

namespace InvoiceMangement.Api.Repository.Interface
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice> GetByIdAsync(int id);
        Task AddAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(int id);
        Task<Invoice> GetInvoiceAsync(string invoiceNumber, DateTime invoiceDate);
        Task SaveInvoiceAsync(Invoice invoice);
    }
}
