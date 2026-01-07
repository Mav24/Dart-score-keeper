using Microsoft.Extensions.Logging;

namespace DartScoreKeeper;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		// Register ViewModels
		builder.Services.AddSingleton<ViewModels.MainViewModel>();
		builder.Services.AddTransient<ViewModels.GameViewModel>();
		
		// Register Views
		builder.Services.AddSingleton<Views.MainPage>();
		builder.Services.AddTransient<Views.GamePage>();

		return builder.Build();
	}
}
