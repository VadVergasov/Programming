using API.Data;
using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab_153503_Verhasau.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _dbContext;

        public CategoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseData<List<Category>>> GetCategoryAsync()
        {
            var categories = _dbContext.Category.ToListAsync();
            return new ResponseData<List<Category>>()
            {
                Data = await categories
            };
        }
    }
}
