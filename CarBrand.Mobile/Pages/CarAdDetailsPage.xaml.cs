using CarBrand.Mobile.ViewModels;

namespace CarBrand.Mobile.Pages;

public partial class CarAdDetailsPage : ContentPage
{
    private readonly CarAdDetailsViewModel _viewModel;

    public CarAdDetailsPage(CarAdDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        await _viewModel.ReloadCarAd();
    }
}