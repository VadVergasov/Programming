using Domain.Entities;
using Domain.Models;
using Lab_153503_Verhasau.Services.CategoryService;

namespace Lab_153503_Verhasau.Services.SouvenirService
{
    public class MemorySouvenirService : ISouvenirService
    {
        private readonly ICategoryService _categoryService;
        private readonly List<Souvenir> _souvenirList;
        public MemorySouvenirService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _souvenirList = new List<Souvenir> {
                new Souvenir {
                    Id = 1,
                    Name = "Ван Гог - Звездная ночь",
                    Category = _categoryService.GetCategoryAsync().Result.Data.Single(x=>x.Name == "Магнит"),
                    Description = "Магнит на холодильник с кассной картиной",
                    Image = "images/night.jpeg",
                    Price = 10,
                },
                new Souvenir {
                    Id = 2,
                    Name = "Микелянджело - Давид",
                    Category = _categoryService.GetCategoryAsync().Result.Data.Single(x=>x.Name == "Открытка"),
                    Description = "Открытка с изображением статуи “Давид” работы Микеланджело. На открытке также может быть надпись “Поздравляем с праздником!” или “С наилучшими пожеланиями!”.",
                    Image = "images/david.jpeg",
                    Price = 5.15M,
                },
            };
        }

        public Task<ResponseData<Souvenir>> CreateSouvenirAsync(Souvenir souvenir, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSouvenirAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Souvenir>> GetSouvenirByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ListModel<Souvenir>>> GetSouvenirListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var result = new ResponseData<ListModel<Souvenir>> { Data = new ListModel<Souvenir> { Items = _souvenirList } };
            return Task.FromResult(result);
        }

        public Task UpdateSouvenirAsync(int id, Souvenir souvenir, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
