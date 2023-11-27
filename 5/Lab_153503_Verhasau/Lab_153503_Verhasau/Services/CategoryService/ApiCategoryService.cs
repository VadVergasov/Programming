using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Lab_153503_Verhasau.Services.CategoryService
{
    public class ApiCategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContext _httpContext;
        private readonly ILogger<ApiCategoryService> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly StringBuilder urlString;

        public ApiCategoryService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiCategoryService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpContext = httpContextAccessor.HttpContext!;
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            urlString = new StringBuilder($"{_httpClient.BaseAddress?.AbsoluteUri}categories/");
        }

        public async Task<ResponseData<List<Category>>> GetCategoryAsync()
        {
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
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
