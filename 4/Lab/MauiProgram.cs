using Lab.Lab3;
using Lab.Lab3.Services;
using Lab.Lab4;
using Lab.Lab4.Services;
using Microsoft.Extensions.Logging;

namespace Lab;

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("BabelStoneFlags.ttf", "Flags");
            });

        builder.Services.AddSingleton<DBView>();
        builder.Services.AddTransient<IDbService, SQLiteService>();
        builder.Services.AddSingleton<Converter>();
        builder.Services.AddHttpClient<IRateService, RateService>(opt => opt.BaseAddress = new Uri("https://www.nbrb.by/api/exrates/rates/"));
        builder.Services.AddHttpClient<ICurrencyService, CurrencyService>(opt => opt.BaseAddress = new Uri("https://www.nbrb.by/api/exrates/currencies"));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
