using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Lab_153503_Verhasau.Services.SouvenirService
{
    public class ApiSouvenirService : ISouvenirService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContext _httpContext;
        private readonly ILogger<ApiSouvenirService> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly int _pageSize;

        public ApiSouvenirService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiSouvenirService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpContext = httpContextAccessor.HttpContext!;
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _pageSize = configuration.GetValue<int>("ItemsPerPage");
        }

        public async Task<ResponseData<ListModel<Souvenir>>> GetSouvenirListAsync(string? categoryNormalizedName, int pageNo = 0)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}souvenirs/");
            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            List<KeyValuePair<string, string?>> options = new()
            {
                new KeyValuePair<string, string?>("pageNo", pageNo.ToString()),
                new KeyValuePair<string, string?>("pageSize", _pageSize.ToString())
            };
            if (categoryNormalizedName != null)
            {
                options.Add(new KeyValuePair<string, string?>("category", categoryNormalizedName));
            }
            urlString.Append(QueryString.Create(options));

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

        public async Task<ResponseData<Souvenir>> GetSouvenirByIdAsync(int id)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}souvenirs/{id}");
            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<Souvenir>>(_jsonSerializerOptions);
                } catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return new ResponseData<Souvenir>
                    {
                        Success = false,
                        ErrorMessage = $"Error: {ex.Message}"
                    };
                }
            }
            _logger.LogError($"No data from server. Error:{response.StatusCode}");
            return new ResponseData<Souvenir>()
            {
                Success = false,
                ErrorMessage = $"No data from server. Error:{response.StatusCode}"
            };
        }

        public async Task UpdateSouvenirAsync(int id, Souvenir souvenir, IFormFile? formFile)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}souvenirs/{id}");
            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            var jsoned = JsonSerializer.Serialize(souvenir, _jsonSerializerOptions);

            var content = new StringContent(jsoned, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(new Uri(urlString.ToString()), content);

            if (response.IsSuccessStatusCode)
            {
                if (formFile is not null)
                {
                    int clothesId = (await response.Content.ReadFromJsonAsync<ResponseData<Souvenir>>(_jsonSerializerOptions))!.Data.Id;
                    await SaveImageAsync(clothesId, formFile);
                }
            } else
            {
                _logger.LogError($"No data from server. Error:{response.StatusCode}");
            }
        }

        public async Task DeleteSouvenirAsync(int id)
        {
            var uriString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}souvenirs/{id}");
            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            var response = await _httpClient.DeleteAsync(new Uri(uriString.ToString()));

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"No data from server. Error:{response.StatusCode}");
            }
        }

        public async Task<ResponseData<Souvenir>> CreateSouvenirAsync(Souvenir souvenir, IFormFile? formFile)
        {
            var uri = new Uri(_httpClient.BaseAddress!.AbsoluteUri + "souvenirs");
            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.PostAsJsonAsync(uri, souvenir, _jsonSerializerOptions);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ResponseData<Souvenir>>(_jsonSerializerOptions);
                if (formFile is not null)
                {
                    await SaveImageAsync(data.Data.Id, formFile);
                }
                return data;
            }
            _logger.LogError($"Object wasn't added. Error:{response.StatusCode}");
            return new ResponseData<Souvenir>
            {
                Success = false,
                ErrorMessage = $"Object wasn't added. Error:{response.StatusCode}"
            };
        }

        private async Task SaveImageAsync(int id, IFormFile image)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_httpClient.BaseAddress?.AbsoluteUri}souvenirs/{id}")
            };
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(image.OpenReadStream());
            content.Add(streamContent, "formFile", image.FileName);
            request.Content = content;
            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            await _httpClient.SendAsync(request);
        }
    }
}
