using CarBrand.Mobile.ViewModels;

namespace CarBrand.Mobile.Pages;

public partial class EditCarAdPage : ContentPage
{
    public EditCarAdPage(EditCarAdViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}