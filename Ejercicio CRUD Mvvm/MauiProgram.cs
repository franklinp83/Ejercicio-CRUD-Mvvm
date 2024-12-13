using Microsoft.Extensions.Logging;

namespace Ejercicio_CRUD_Mvvm
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        // Ruta de la base de datos
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "proveedores.db");

        builder
            .UseMauiApp(() => new App(dbPath))
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}
