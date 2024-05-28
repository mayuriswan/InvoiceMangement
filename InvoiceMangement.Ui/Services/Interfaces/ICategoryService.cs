
using InvoiceMangement.Shared.Models;

namespace InvoiceMangement.Ui.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();
    }

}
