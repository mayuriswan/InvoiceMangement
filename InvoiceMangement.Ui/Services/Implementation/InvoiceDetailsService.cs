using InvoiceMangement.Api.Models;
using InvoiceMangement.Ui.Services.Interfaces;

namespace InvoiceMangement.Ui.Services.Implementation
{
    public class InvoiceDetailsService : IInvoiceDetailsService
    {
        private readonly HttpClient _httpClient;

        public InvoiceDetailsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<InvoiceDetails>> GetInvoiceDetailsByInvoiceIdAsync(int invoiceId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<InvoiceDetails>>($"api/invoicedetails/by-invoice/{invoiceId}");
        }

        public async Task<InvoiceDetails> GetInvoiceDetailsByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<InvoiceDetails>($"api/invoicedetails/{id}");
        }

        public async Task AddInvoiceDetailsAsync(InvoiceDetails invoiceDetails)
        {
            await _httpClient.PostAsJsonAsync("api/invoicedetails", invoiceDetails);
        }

        public async Task UpdateInvoiceDetailsAsync(InvoiceDetails invoiceDetails)
        {
            await _httpClient.PutAsJsonAsync($"api/invoicedetails/{invoiceDetails.DetailID}", invoiceDetails);
        }

        public async Task DeleteInvoiceDetailsAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/invoicedetails/{id}");
        }
    }
}
