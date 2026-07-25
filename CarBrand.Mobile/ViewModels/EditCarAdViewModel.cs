using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using CarBrand.Domain;
using CarBrand.Application.UseCases.CarAds.Commands;
using CarBrand.Mobile.Messages;

namespace CarBrand.Mobile.ViewModels;

[QueryProperty(nameof(CarAd), "CarAd")]
public partial class EditCarAdViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    public EditCarAdViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    private CarAd? _carAd; 

    public CarAd? CarAd
    {
        get => _carAd;
        set
        {
            _carAd = value;
            if (value is not null)
            {
                AdTitle = value.Title;
                Price   = value.Price;
                Year    = value.Year;
                Mileage = value.Mileage;
                Color   = value.Color;
            }
        }
    }

    [ObservableProperty] string adTitle  = string.Empty;
    [ObservableProperty] decimal price;
    [ObservableProperty] int year;
    [ObservableProperty] int mileage;
    [ObservableProperty] string color = string.Empty;

    [RelayCommand]
    async Task Save()
    {
        if (_carAd is null) return;

        if (string.IsNullOrWhiteSpace(AdTitle) ||
            string.IsNullOrWhiteSpace(Color)   ||
            Year < 1900 || Mileage < 0 || Price < 0)
        {
            await Shell.Current.DisplayAlert(
                "Ошибка", "Проверьте правильность заполнения полей", "OK");
            return;
        }

        await _mediator.Send(new UpdateCarAdCommand(
            _carAd.Id, AdTitle, Price, Year, Mileage, Color));

        WeakReferenceMessenger.Default.Send(new CarAdChangedMessage(_carAd.Id));

        await Shell.Current.GoToAsync("..");
    }
}