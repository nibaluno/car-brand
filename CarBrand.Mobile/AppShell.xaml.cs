using CarBrand.Mobile.Pages;

namespace CarBrand.Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(CarAdDetailsPage), typeof(CarAdDetailsPage));
        Routing.RegisterRoute(nameof(EditCarAdPage), typeof(EditCarAdPage)); 
        Routing.RegisterRoute(nameof(AddCarBrandPage), typeof(AddCarBrandPage));
        Routing.RegisterRoute(nameof(AddCarAdPage), typeof(AddCarAdPage));
    }
}