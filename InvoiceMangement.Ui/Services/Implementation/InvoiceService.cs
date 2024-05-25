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
            var response = await _httpClient.PostAsJsonAsync("api/invoice", invoice);

            if (response.IsSuccessStatusCode)
            {
                // The request was successful.
                // You can log the success or return from the method if needed.
                return;
            }
            else
            {
                // The request failed.
                // Read the error details from the response.
                var errorContent = await response.Content.ReadAsStringAsync();

                // You can log the error content, throw an exception, or handle it as needed.
                throw new ApplicationException($"Error adding invoice: {errorContent}");
            }
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
           var reponse =  await _httpClient.PutAsJsonAsync($"api/invoice/{invoice.InvoiceID}", invoice);
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/invoice/{id}");
        }
        public async Task<Invoice> GetInvoiceByStoredProcedureAsync(string invoiceNumber, DateTime invoiceDate)
        {
            var response = await _httpClient.GetFromJsonAsync<Invoice>($"api/invoice/byInvoiceNumber?invoiceNumber={invoiceNumber}&invoiceDate={invoiceDate:yyyy-MM-dd}");
            return response;
        }
        public async Task SaveSpInvoiceAsync(Invoice invoice)
        {
            var response = await _httpClient.PostAsJsonAsync("api/invoice/save", invoice);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Error adding invoice: {errorContent}");
            }
        }
    }
}
