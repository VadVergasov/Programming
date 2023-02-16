using Lab.Lab4.Entities;
using System.Net.Http.Json;

namespace Lab.Lab4.Services {
    public class CurrencyService : ICurrencyService {
        private readonly HttpClient Client;

        public CurrencyService(HttpClient client) {
            Client = client;
        }

        public IEnumerable<Currency> GetCurrencies() {
            var result = Client.Send(new HttpRequestMessage(HttpMethod.Get, ""));
            var response = HttpContentJsonExtensions.ReadFromJsonAsync<IEnumerable<Currency>>(result.Content);
            response.Wait();
            IEnumerable<Currency> currencies = response.Result!.Where(currency => currency.Cur_DateEnd > DateTime.Today);
            return currencies;
        }
    }
}
