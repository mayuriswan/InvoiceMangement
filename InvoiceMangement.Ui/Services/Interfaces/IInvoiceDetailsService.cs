
using InvoiceMangement.Shared.Models;

namespace InvoiceMangement.Ui.Services.Interfaces
{
    public interface IInvoiceDetailsService
    {
        Task<IEnumerable<InvoiceDetails>> GetInvoiceDetailsByInvoiceIdAsync(int invoiceId);
        Task<InvoiceDetails> GetInvoiceDetailsByIdAsync(int id);
        Task AddInvoiceDetailsAsync(InvoiceDetails invoiceDetails);
        Task UpdateInvoiceDetailsAsync(InvoiceDetails invoiceDetails);
        Task DeleteInvoiceDetailsAsync(int id);
    }
}
