using Domain.Entities;
using Domain.Models;

namespace Lab_153503_Verhasau.Services.CategoryService
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryAsync()
        {
            var categories = new List<Category> {
                new Category { Id = 1, Name = "Магнит", NormalizedName = "magneet"},
                new Category { Id = 2, Name = "Открытка", NormalizedName = "otkritka"}
            };
            var result = new ResponseData<List<Category>> { Data = categories };
            return Task.FromResult(result);
        }
    }
}
