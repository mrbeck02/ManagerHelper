using ManagerHelper.ViewModels;
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

        return builder.Build();
	}
}
