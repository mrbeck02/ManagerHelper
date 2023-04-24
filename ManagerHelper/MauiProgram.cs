using ManagerHelper.CsvImporter;
using ManagerHelper.Data;
using ManagerHelper.Jira;
using ManagerHelper.ViewModels;
using ManagerHelper.ViewModels.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

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
		
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddScoped<IMainViewModel, MainViewModel>();
        builder.Services.AddScoped<IDesignTimeDbContextFactory<DataContext>, DesignTimeDataContextFactory>();
        builder.Services.AddScoped<IAlertService, AlertService>();
        builder.Services.AddScoped<ISqliteDataContextFactory<DataContext>, SqliteDataContextFactory>();
        builder.Services.AddScoped<IStatisticsCsvReader, StatisticsCsvReader>();
        builder.Services.AddScoped<IStatisticsCsvImporter, StatisticsCsvImporter>();

        return builder.Build();
	}
}
