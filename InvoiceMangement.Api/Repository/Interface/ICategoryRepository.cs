using InvoiceMangement.Api.Models;

namespace InvoiceMangement.Api.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}

