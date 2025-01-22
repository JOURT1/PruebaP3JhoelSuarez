using Microsoft.Extensions.Logging;
using PruebaP3JhoelSuarez.Interfaces;
using PruebaP3JhoelSuarez.Repositories;
using PruebaP3JhoelSuarez.View;
using PruebaP3JhoelSuarez.ViewModel;

namespace PruebaP3JhoelSuarez
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "JsuarezBDD1.db");
            builder.Services.AddSingleton<IBDDJsuarez>(s => new BDDJsuarezRepository(dbPath));
            builder.Services.AddSingleton<APIJsuarezViewModel>();
            builder.Services.AddSingleton<BDDJsuarezViewModel>();
            builder.Services.AddSingleton<APIJsuarez>();
            builder.Services.AddSingleton<BDDJsuarez>();

            return builder.Build();
        }
    }
}
