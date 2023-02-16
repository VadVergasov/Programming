using Lab.Lab4.Entities;

namespace Lab.Lab4.Services {
    public interface ICurrencyService {
        public IEnumerable<Currency> GetCurrencies();
    }
}
