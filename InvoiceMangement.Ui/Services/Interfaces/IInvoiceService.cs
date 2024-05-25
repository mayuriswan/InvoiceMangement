using InvoiceMangement.Api.Models;

namespace InvoiceMangement.Ui.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task<Invoice> GetInvoiceByIdAsync(int id);
        Task AddInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(int id);
        Task<Invoice> GetInvoiceByStoredProcedureAsync(string invoiceNumber, DateTime invoiceDate);
        Task SaveSpInvoiceAsync(Invoice invoice);
    }
}
