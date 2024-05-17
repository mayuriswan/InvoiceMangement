using InvoiceMangement.Api.Models;

namespace InvoiceMangement.Api.Repository.Interface
{
    public interface IInvoiceHeaderRepository
    {
        Task<IEnumerable<InvoiceHeader>> GetAllAsync();
        Task<InvoiceHeader> GetByIdAsync(int id);
        Task AddAsync(InvoiceHeader invoiceHeader);
        Task UpdateAsync(InvoiceHeader invoiceHeader);
        Task DeleteAsync(int id);
    }
}
