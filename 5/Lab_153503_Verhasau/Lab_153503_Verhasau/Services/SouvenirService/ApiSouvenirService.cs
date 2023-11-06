﻿using Domain.Entities;
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

            var response = await _httpClient.PutAsync(new Uri(urlString.ToString()),
                new StringContent(JsonSerializer.Serialize(souvenir), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                if (formFile is not null)
                {
                    int clothesId = (await response.Content.ReadFromJsonAsync<ResponseData<Souvenir>>(_jsonSerializerOptions)).Data.Id;
                }
            } else
            {
                _logger.LogError($"No data from server. Error:{response.StatusCode}");
            }
        }

        public async Task DeleteSouvenirAsync(int id)
        {
            var uriString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}souvenir/{id}");

            var response = await _httpClient.DeleteAsync(new Uri(uriString.ToString()));

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"No data from server. Error:{response.StatusCode}");
            }
        }

        public async Task<ResponseData<Souvenir>> CreateSouvenirAsync(Souvenir souvenir, IFormFile? formFile)
        {
            var uri = new Uri(_httpClient.BaseAddress!.AbsoluteUri + "Clothes");
            var response = await _httpClient.PostAsJsonAsync(uri, souvenir, _jsonSerializerOptions);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ResponseData<Souvenir>>(_jsonSerializerOptions);
                return data;
            }
            _logger.LogError($"Object wasn't added. Error:{response.StatusCode}");
            return new ResponseData<Souvenir>
            {
                Success = false,
                ErrorMessage = $"Object wasn't added. Error:{response.StatusCode}"
            };
        }
    }
}
