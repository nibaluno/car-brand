using MediatR;
using CarBrand.Domain;
using CarBrand.Application.UseCases.CarBrands.Queries;
using CarBrand.Application.UseCases.CarAds.Queries;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CarBrand.Mobile.Pages;
using CarBrand.Mobile.Messages;

namespace CarBrand.Mobile.ViewModels;

public partial class CarBrandsViewModel : ObservableObject, IRecipient<CarAdChangedMessage>
{
    private readonly IMediator _mediator;

    public CarBrandsViewModel(IMediator mediator)
    {
        _mediator = mediator;
        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(CarAdChangedMessage message)
    {
        MainThread.BeginInvokeOnMainThread(async () => 
        {
            await GetCarBrands();
            await GetCarAds();
        });
    }

    [RelayCommand]
    async Task AddBrand() => await Shell.Current.GoToAsync(nameof(AddCarBrandPage));

    [RelayCommand]
    async Task AddAd()
    {
        if (SelectedCarBrand == null)
        {
            await Shell.Current.DisplayAlert("Внимание", "Сначала выберите марку", "OK");
            return;
        }

        var parameters = new Dictionary<string, object> { { "BrandId", SelectedCarBrand.Id } };
        await Shell.Current.GoToAsync(nameof(AddCarAdPage), parameters);
    }

    public ObservableCollection<CarBrand.Domain.CarBrand> CarBrands { get; set; } = new();
    public ObservableCollection<CarAd> CarAds { get; set; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsBrandSelected))]
    CarBrand.Domain.CarBrand? selectedCarBrand;

    public bool IsBrandSelected => SelectedCarBrand != null;

    [RelayCommand]
    async Task UpdateGroupList() => await GetCarBrands();

    [RelayCommand]
    async Task UpdateMembersList() => await GetCarAds();

    public async Task GetCarBrands()
    {
        var currentSelectionId = SelectedCarBrand?.Id;
        
        var brands = await _mediator.Send(new GetAllCarBrandsRequest());
        
        CarBrands.Clear();
        foreach (var brand in brands) CarBrands.Add(brand);

        if (currentSelectionId.HasValue)
        {
            SelectedCarBrand = CarBrands.FirstOrDefault(b => b.Id == currentSelectionId.Value);
        }
    }

    public async Task GetCarAds()
    {
        if (SelectedCarBrand == null) 
        {
            CarAds.Clear();
            return;
        }

        var ads = await _mediator.Send(new GetCarAdsByBrandRequest(SelectedCarBrand.Id));

        CarAds.Clear();
        foreach (var ad in ads) CarAds.Add(ad);
    }
    
    [RelayCommand]
    async Task ShowDetails(CarAd ad)
    {
        if (ad == null) return;
        var parameters = new Dictionary<string, object> { { "CarAd", ad } };
        await Shell.Current.GoToAsync(nameof(CarAdDetailsPage), parameters);
    }
}