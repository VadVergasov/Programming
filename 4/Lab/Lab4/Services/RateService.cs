using Lab.Lab4.Entities;
using System.Net.Http.Json;

namespace Lab.Lab4.Services {
    public class RateService : IRateService {
        private HttpClient Client { get; init; }

        public RateService(HttpClient client) {
            Client = client;
        }

        public Rate GetRate(DateTime date, Currency currency) {
            var result = Client.Send(new HttpRequestMessage(HttpMethod.Get, $"{currency.Cur_ID}?ondate={date:yyyy-M-d}"));
            var response = HttpContentJsonExtensions.ReadFromJsonAsync<Rate>(result.Content);
            response.Wait();
            return response.Result!;
        }
    }
}
