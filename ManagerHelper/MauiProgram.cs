using ManagerHelper.CsvImporter;
using ManagerHelper.Data;
using ManagerHelper.Pages;
using ManagerHelper.ViewModels;
using ManagerHelper.ViewModels.Support;

namespace ManagerHelper;

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

		builder.Services.AddSingleton<ImportFromCsvPage>();
        builder.Services.AddSingleton<SetupDatastorePage>();
        builder.Services.AddScoped<IImportFromCsvViewModel, ImportFromCsvViewModel>();
        builder.Services.AddScoped<ISetupDatastoreViewModel, SetupDatastoreViewModel>();
        //builder.Services.AddScoped<IDesignTimeDbContextFactory<DataContext>, DesignTimeDataContextFactory>();
        builder.Services.AddScoped<IAlertService, AlertService>();
        builder.Services.AddScoped<ISqliteDataContextFactory<DataContext>, SqliteDataContextFactory>();
        builder.Services.AddScoped<IStatisticsCsvReader, StatisticsCsvReader>();
        builder.Services.AddScoped<IStatisticsCsvImporter, StatisticsCsvImporter>();

        return builder.Build();
	}
}
