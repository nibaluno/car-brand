using CarBrand.Mobile.ViewModels;

namespace CarBrand.Mobile.Pages;

public partial class CarBrandsPage : ContentPage
{
    private readonly CarBrandsViewModel _viewModel;

    public CarBrandsPage(CarBrandsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var selectedId = _viewModel.SelectedCarBrand?.Id;

        await _viewModel.GetCarBrands();

        if (selectedId.HasValue)
        {
            var restoredBrand = _viewModel.CarBrands.FirstOrDefault(b => b.Id == selectedId.Value);
            if (restoredBrand != null)
            {
                _viewModel.SelectedCarBrand = restoredBrand;
                await _viewModel.GetCarAds();
            }
        }
    }
}