using Domain.Entities;
using Domain.Models;

namespace Lab_153503_Verhasau.Services.SouvenirService
{
    public interface ISouvenirService
    {
        public Task<ResponseData<ListModel<Souvenir>>> GetSouvenirListAsync(string? categoryNormalizedName, int pageNo = 0);

        public Task<ResponseData<Souvenir>> GetSouvenirByIdAsync(int id);

        public Task UpdateSouvenirAsync(int id, Souvenir souvenir, IFormFile? formFile);

        public Task DeleteSouvenirAsync(int id);

        public Task<ResponseData<Souvenir>> CreateSouvenirAsync(Souvenir souvenir, IFormFile? formFile);
    }
}
