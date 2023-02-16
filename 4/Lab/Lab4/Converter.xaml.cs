using Lab.Lab4.Entities;
using Lab.Lab4.Services;
using System.Collections.ObjectModel;

namespace Lab.Lab4;

public partial class Converter : ContentPage {
    private IRateService Rates { get; init; }
    private ICurrencyService Currencies { get; init; }
    public DateTime Today { get; init; }
    public IList<Currency> CurrenciesList { get; set; }
    public ObservableCollection<Rate> RateList { get; set; } = new();

    private void ConvertBack(object? sender, TextChangedEventArgs e) {
        if (CurrencyPicker.SelectedItem is null || e.NewTextValue.Select(symbol => char.IsDigit(symbol) || symbol == ',' ).Count() != e.NewTextValue.Length) {
            return;
        }
        if (Result.Text.Length == 0) {
            Value.TextChanged -= ConvertValue;
            Value.Text = "";
            Value.TextChanged += ConvertValue;
            return;
        }
        Rate rate = Rates.GetRate(Date.Date, (CurrencyPicker.SelectedItem as Currency)!);
        Value.TextChanged -= ConvertValue;
        Value.Text = $"{Convert.ToDecimal(Result.Text) / rate.Cur_OfficialRate * rate.Cur_Scale:0.##}";
        Value.TextChanged += ConvertValue;
    }

    private void ConvertValue(object? sender, TextChangedEventArgs e) {
        if (CurrencyPicker.SelectedItem is null || e.NewTextValue.Select(symbol => char.IsDigit(symbol) || symbol == ',').Count() != e.NewTextValue.Length) {
            return;
        }
        if (Value.Text.Length == 0) {
            Result.TextChanged -= ConvertValue;
            Result.Text = "";
            Result.TextChanged += ConvertValue;
            return;
        }
        Rate rate = Rates.GetRate(Date.Date, (CurrencyPicker.SelectedItem as Currency)!);
        Result.TextChanged -= ConvertBack;
        Result.Text = $"{rate.Cur_OfficialRate * Convert.ToDecimal(Value.Text) / rate.Cur_Scale:0.##}";
        Result.TextChanged += ConvertBack;
    }

    private void ClearInput(object? sender, EventArgs e) {
        Result.TextChanged -= ConvertBack;
        Value.TextChanged -= ConvertValue;
        Result.Text = "";
        Value.Text = "";
        Result.TextChanged += ConvertBack;
        Value.TextChanged += ConvertValue;
    }

    private void UpdateRates() {
        var ratesList = new List<Rate>();
        foreach (var currency in CurrenciesList) {
            ratesList.Add(Rates.GetRate(Today, currency));
        }
        foreach (var rate in ratesList) {
            MainThread.InvokeOnMainThreadAsync(() => RateList.Add(rate));
        }
    }

    private void LoadCurrencies(object? sender, EventArgs e) {
        Task.Run(() => { CurrenciesList = Currencies.GetCurrencies().ToList(); }).ContinueWith((result) => {
            MainThread.InvokeOnMainThreadAsync(() => {
                CurrencyPicker.ItemsSource = null;
                CurrencyPicker.ItemsSource = (System.Collections.IList?)CurrenciesList;
                Task.Run(UpdateRates).ContinueWith((result) => MainThread.InvokeOnMainThreadAsync(() => Activity.IsRunning = false));
            });
        });
    }

    public Converter(IRateService rates, ICurrencyService currencies) {
        Today = DateTime.Today;
        Rates = rates;
        Currencies = currencies;
        InitializeComponent();
        CurrenciesList = new List<Currency>();
        BindingContext = this;
    }

    private void DateSelected(object sender, DateChangedEventArgs e) {
        ConvertValue(sender, new TextChangedEventArgs(Value.Text, Value.Text));
    }
}