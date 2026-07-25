using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CarBrand.Application;
using CarBrand.Persistense;
using CarBrand.Persistense.Data; 
using CommunityToolkit.Maui;
using CarBrand.Mobile.Pages;
using CarBrand.Mobile.ViewModels;
using Java.Nio.FileNio;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using FileSystem = Microsoft.Maui.Storage.FileSystem;

namespace CarBrand.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        string settingsStream = "CarBrand.Mobile.appsettings.json";
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream(settingsStream);
        if (stream != null) 
            builder.Configuration.AddJsonStream(stream);


        var connStr = builder.Configuration.GetConnectionString("SqliteConnection");
        
        string dataDirectory = FileSystem.Current.AppDataDirectory + "/";
        connStr = String.Format(connStr, dataDirectory);

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connStr)
            .Options;

        builder.Services
            .AddApplication()
            .AddPersistence(options) 
            .RegisterPages()
            .RegisterViewModels();
        

        DbInitializer.Initialize(builder.Services.BuildServiceProvider()).Wait();
        return builder.Build();
    }

    static IServiceCollection RegisterPages(this IServiceCollection services)
    {
        services.AddTransient<CarBrandsPage>();
        services.AddTransient<CarAdDetailsPage>();
        services.AddTransient<EditCarAdPage>();
        services.AddTransient<AddCarBrandPage>(); 
        services.AddTransient<AddCarAdPage>();   
        return services;
    }

    static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
        services.AddTransient<CarBrandsViewModel>();
        services.AddTransient<CarAdDetailsViewModel>();
        services.AddTransient<EditCarAdViewModel>();
        services.AddTransient<AddCarBrandViewModel>(); 
        services.AddTransient<AddCarAdViewModel>();   
        return services;
    }
}