using Domain.Entities;
using Domain.Models;
using System.Text;
using System.Text.Json;

namespace Lab_153503_Verhasau.Services.CategoryService
{
    public class ApiCategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiCategoryService> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
		private readonly StringBuilder urlString;

		public ApiCategoryService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiCategoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            urlString = new StringBuilder($"{_httpClient.BaseAddress?.AbsoluteUri}categories/");
		}

        public async Task<ResponseData<List<Category>>> GetCategoryAsync()
        {
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<List<Category>>>(_jsonSerializerOptions);
                } catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return new ResponseData<List<Category>>
                    {
                        Success = false,
                        ErrorMessage = $"Error: {ex.Message}"
                    };

                }
            }
            _logger.LogError($"-----> No response from server. Error:{response.StatusCode}");
            return new ResponseData<List<Category>>()
            {
                Success = false,
                ErrorMessage = $"No response from server. Error:{response.StatusCode}"
            };
        }
    }
}
