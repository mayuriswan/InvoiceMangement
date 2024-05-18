using InvoiceMangement.Api.Models;
using InvoiceMangement.Ui.Services.Interfaces;

namespace InvoiceMangement.Ui.Services.Implementation
{
    public class InvoiceService : IInvoiceService
    {
        private readonly HttpClient _httpClient;

        public InvoiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Invoice>>("api/invoice");
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Invoice>($"api/invoice/{id}");
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            await _httpClient.PostAsJsonAsync("api/invoice", invoice);
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            await _httpClient.PutAsJsonAsync($"api/invoice/{invoice.InvoiceID}", invoice);
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/invoice/{id}");
        }
    }
}
