using API.Data;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services.SouvenirService
{
    public class SouvenirService : ISouvenirService
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly int _maxPageSize = 20;

        public SouvenirService([FromServices] IConfiguration config, AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = appDbContext;
            _configuration = config;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseData<Souvenir>> CreateSouvenirAsync(Souvenir souvenir, IFormFile? formFile)
        {
            _dbContext.Category.Attach(souvenir.Category);
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
            var query = _dbContext.Souvenir.Include(x => x.Category).AsQueryable();
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

            dataList.Items = await query.Skip(pageNo * pageSize).Take(pageSize).ToListAsync();

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
            target.Category = souvenir.Category;
            _dbContext.Entry(target).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            var responseData = new ResponseData<string>();
            var souvenir = await _dbContext.Souvenir.FindAsync(id);
            if (souvenir == null)
            {
                responseData.Success = false;
                responseData.ErrorMessage = "No item found";
                return responseData;
            }
            var host = "https://" + _httpContextAccessor.HttpContext?.Request.Host;
            var imageFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

            if (formFile != null)
            {
                if (!string.IsNullOrEmpty(souvenir.Image))
                {
                    var prevImage = Path.GetFileName(souvenir.Image);
                    var prevImagePath = Path.Combine(imageFolder, prevImage);
                    if (File.Exists(prevImagePath))
                    {
                        File.Delete(prevImagePath);
                    }
                }
                var ext = Path.GetExtension(formFile.FileName);
                var fName = Path.ChangeExtension(Path.GetRandomFileName(), ext);
                var filePath = Path.Combine(imageFolder, fName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                souvenir.Image = $"{host}/images/{fName}";
                _dbContext.Entry(souvenir).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            responseData.Data = souvenir.Image;
            return responseData;
        }
    }
}
