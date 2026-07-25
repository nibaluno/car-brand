using CarBrand.Mobile.ViewModels;

namespace CarBrand.Mobile.Pages;

public partial class AddCarBrandPage : ContentPage
{
    public AddCarBrandPage(AddCarBrandViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}