using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using CarBrand.Application.UseCases.CarBrands.Commands;
using CarBrand.Mobile.Messages;

namespace CarBrand.Mobile.ViewModels;

public partial class AddCarBrandViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    public AddCarBrandViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ObservableProperty] string name = string.Empty;
    [ObservableProperty] string country = string.Empty;

    [RelayCommand]
    async Task Save()
    {
        if (string.IsNullOrWhiteSpace(Name) ||
            string.IsNullOrWhiteSpace(Country))
        {
            await Shell.Current.DisplayAlert(
                "Ошибка", "Заполните все поля", "OK");
            return;
        }

        await _mediator.Send(new AddCarBrandCommand(Name, Country));

        WeakReferenceMessenger.Default.Send(new CarAdChangedMessage(0));

        await Shell.Current.GoToAsync("..");
    }
}