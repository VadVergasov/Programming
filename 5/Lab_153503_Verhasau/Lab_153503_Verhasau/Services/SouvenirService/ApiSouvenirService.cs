using Domain.Entities;
using Domain.Models;
using System.Text;
using System.Text.Json;

namespace Lab_153503_Verhasau.Services.SouvenirService
{
    public class ApiSouvenirService : ISouvenirService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiSouvenirService> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly int _pageSize;

        public ApiSouvenirService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiSouvenirService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _pageSize = configuration.GetValue<int>("ItemsPerPage");
        }

        public async Task<ResponseData<ListModel<Souvenir>>> GetSouvenirListAsync(string? categoryNormalizedName, int pageNo = 0)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}souvenirs/");
            if (categoryNormalizedName != null)
            {
                urlString.Append($"{categoryNormalizedName}/");
            };
            urlString.Append($"page{pageNo}");
            if (!_pageSize.Equals("3"))
            {
                urlString.Append(QueryString.Create("pageSize", _pageSize.ToString()));
            }

            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Souvenir>>>(_jsonSerializerOptions);
                } catch (JsonException ex)
                {
                    _logger.LogError($"-----> Error: {ex.Message}");
                    return new ResponseData<ListModel<Souvenir>>
                    {
                        Success = false,
                        ErrorMessage = $"Error: {ex.Message}"
                    };
                }
            }
            _logger.LogError($"-----> Данные не получены от сервера. Error:{response.StatusCode}");
            return new ResponseData<ListModel<Souvenir>>()
            {
                Success = false,
                ErrorMessage = $"No response from server. Error:{response.StatusCode}"
            };
        }

        public Task<ResponseData<Souvenir>> GetSouvenirByIdAsync(int id) => throw new NotImplementedException();
        public Task UpdateSouvenirAsync(int id, Souvenir souvenir, IFormFile? formFile) => throw new NotImplementedException();
        public Task DeleteSouvenirAsync(int id) => throw new NotImplementedException();
        public Task<ResponseData<Souvenir>> CreateSouvenirAsync(Souvenir souvenir, IFormFile? formFile) => throw new NotImplementedException();
    }
}
