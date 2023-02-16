using Lab.Lab4.Entities;

namespace Lab.Lab4.Services {
    public interface IRateService {
        Rate GetRate(DateTime date, Currency currency);
    }
}
