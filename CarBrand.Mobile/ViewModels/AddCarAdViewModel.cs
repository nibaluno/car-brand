using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using CarBrand.Application.UseCases.CarAds.Commands;
using CarBrand.Mobile.Messages;

namespace CarBrand.Mobile.ViewModels;

[QueryProperty(nameof(BrandId), "BrandId")]
public partial class AddCarAdViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    public AddCarAdViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ObservableProperty] int brandId;
    [ObservableProperty] string title  = string.Empty;
    [ObservableProperty] string price  = string.Empty;
    [ObservableProperty] string year   = string.Empty;
    [ObservableProperty] string mileage = string.Empty;
    [ObservableProperty] string color  = string.Empty;

    [RelayCommand]
    async Task Save()
    {
        if (string.IsNullOrWhiteSpace(Title) ||
            !decimal.TryParse(Price,   out var p)  ||
            !int.TryParse(Year,        out var y)   ||
            !int.TryParse(Mileage,     out var m)||
            string.IsNullOrWhiteSpace(Color))
        {
            await Shell.Current.DisplayAlert(
                "Ошибка", "Проверьте правильность заполнения полей", "OK");
            return;
        }

        await _mediator.Send(new AddCarAdCommand(
            Title, p, y, m, Color, BrandId));

        WeakReferenceMessenger.Default.Send(new CarAdChangedMessage(0));

        await Shell.Current.GoToAsync("..");
    }
}