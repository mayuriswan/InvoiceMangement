using InvoiceMangement.Api.Data;
using InvoiceMangement.Api.Models;
using InvoiceMangement.Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace InvoiceMangement.Api.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
