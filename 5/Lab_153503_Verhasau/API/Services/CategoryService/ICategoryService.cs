using Domain.Entities;
using Domain.Models;

namespace Lab_153503_Verhasau.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryAsync();
    }
}
