using InvoiceMangement.Api.Models;
using InvoiceMangement.Ui.Services.Interfaces;

namespace InvoiceMangement.Ui.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>("api/categories");
        }
    }
}
