using API.Data;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_153503_Verhasau.Services.SouvenirService
{
    public class SouvenirService : ISouvenirService
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly int _maxPageSize = 20;

        public SouvenirService([FromServices] IConfiguration config, AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
            _configuration = config;
        }

        public async Task<ResponseData<Souvenir>> CreateSouvenirAsync(Souvenir souvenir, IFormFile? formFile)
        {
            _dbContext.Souvenir.Add(souvenir);
            try
            {
                await _dbContext.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex)
            {
                return new ResponseData<Souvenir>()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
            return new ResponseData<Souvenir>()
            {
                Data = souvenir
            };
        }

        public async Task DeleteSouvenirAsync(int id)
        {
            var clothes = await _dbContext.Souvenir.FindAsync(id) ?? throw new ArgumentException($"No souvenir with such id: {id}");
            _dbContext.Souvenir.Remove(clothes);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ResponseData<Souvenir>> GetSouvenirByIdAsync(int id)
        {
            var souvenir = await _dbContext.Souvenir.FindAsync(id);
            if (souvenir is null)
            {
                return new ResponseData<Souvenir>()
                {
                    Success = false,
                    ErrorMessage = $"No souvenir with such id: {id}"
                };
            }

            return new ResponseData<Souvenir>()
            {
                Data = souvenir
            };
        }

        public async Task<ResponseData<ListModel<Souvenir>>> GetSouvenirListAsync(string? categoryNormalizedName, int pageNo = 0, int pageSize = 3)
        {
            if (pageSize > _maxPageSize)
            {
                pageSize = _maxPageSize;
            }
            var query = _dbContext.Souvenir.AsQueryable();
            var dataList = new ListModel<Souvenir>();
            query = query.Where(d => categoryNormalizedName == null || d.Category.NormalizedName.Equals(categoryNormalizedName));
            var count = query.Count();
            if (count == 0)
            {
                return new ResponseData<ListModel<Souvenir>>
                {
                    Data = dataList
                };
            }
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);
            if (pageNo > totalPages)
            {
                return new ResponseData<ListModel<Souvenir>>
                {
                    Success = false,
                    ErrorMessage = "Нет такой страницы"
                };
            }

            dataList.Items = await query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();

            dataList.CurrentPage = pageNo;
            dataList.TotalPages = totalPages;
            var response = new ResponseData<ListModel<Souvenir>>
            {
                Data = dataList
            };
            return response;
        }

        public async Task UpdateSouvenirAsync(int id, Souvenir souvenir, IFormFile? formFile)
        {
            var target = await _dbContext.Souvenir.FindAsync(id) ?? throw new ArgumentException($"No souvenir with such id: {id}");
            target.Name = souvenir.Name;
            target.Description = souvenir.Description;
            target.Price = souvenir.Price;
            target.Image = souvenir.Image;
            target.Category = souvenir.Category;
            _dbContext.Entry(target).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
