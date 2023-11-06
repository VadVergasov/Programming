using Domain.Entities;
using Domain.Models;

namespace API.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryAsync();
    }
}
