using CarBrand.Mobile.ViewModels;

namespace CarBrand.Mobile.Pages;

public partial class AddCarAdPage : ContentPage
{
    public AddCarAdPage(AddCarAdViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}