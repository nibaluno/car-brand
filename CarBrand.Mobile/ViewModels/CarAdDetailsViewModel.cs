using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using CarBrand.Domain;
using CarBrand.Application.UseCases.CarAds.Commands;
using CarBrand.Application.UseCases.CarAds.Queries;
using CarBrand.Application.UseCases.CarBrands.Queries;
using CarBrand.Mobile.Messages;

namespace CarBrand.Mobile.ViewModels;

[QueryProperty(nameof(CarAd), "CarAd")]
public partial class CarAdDetailsViewModel : ObservableObject, IRecipient<CarAdChangedMessage>
{
    private readonly IMediator _mediator;
    private int _currentAdId;

    private static string ImagesFolder =>
        Path.Combine(FileSystem.Current.AppDataDirectory, "Images");

    public CarAdDetailsViewModel(IMediator mediator)
    {
        _mediator = mediator;
        WeakReferenceMessenger.Default.Register(this);
    }

    
    public void Receive(CarAdChangedMessage message)
    {
        if (message.Id == 0 || message.Id == _currentAdId)
        {
            MainThread.BeginInvokeOnMainThread(async () => await ReloadCarAd());
        }
    }

    [ObservableProperty] CarAd? _carAd;
    [ObservableProperty] ImageSource? _carImage;

    partial void OnCarAdChanged(CarAd? value)
    {
        if (value is not null)
        {
            _currentAdId = value.Id;
            RefreshImage(value.Id);
        }
    }

    public async Task ReloadCarAd()
    {
        if (_currentAdId <= 0) return;

        var fresh = await _mediator.Send(new GetCarAdByIdRequest(_currentAdId));
        if (fresh is null) return;

        await MainThread.InvokeOnMainThreadAsync(() =>
        {
  
            CarAd = null; 
    
            CarAd = fresh; 
            
        });
    }

    private void RefreshImage(int id)
    {
        CarImage = null; 

        foreach (var ext in new[] { ".jpg", ".jpeg", ".png", ".heic" })
        {
            var path = Path.Combine(ImagesFolder, $"{id}{ext}");
            if (!File.Exists(path)) continue;

            try 
            {
                var bytes = File.ReadAllBytes(path);
                CarImage = ImageSource.FromStream(() => new MemoryStream(bytes));
                return;
            }
            catch { continue; }
        }

        CarImage = "no_image.png";
    }

    [RelayCommand]
    async Task DeleteAd()
    {
        if (CarAd is null) return;

        bool confirm = await Shell.Current.DisplayAlert("Удаление", $"Удалить объявление \"{CarAd.Title}\"?", "Да", "Нет");
        if (!confirm) return;

        await _mediator.Send(new DeleteCarAdCommand(CarAd.Id));
        
        WeakReferenceMessenger.Default.Send(new CarAdChangedMessage(0));
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task EditAd()
    {
        if (CarAd is null) return;
        var parameters = new Dictionary<string, object> { { "CarAd", CarAd } };
        await Shell.Current.GoToAsync("EditCarAdPage", parameters);
    }

    [RelayCommand]
    async Task MoveToBrand()
    {
        if (CarAd is null) return;

        var brands = (await _mediator.Send(new GetAllCarBrandsRequest())).ToList();
        var brandNames = brands.Select(b => b.Name).ToArray();

        string? selected = await Shell.Current.DisplayActionSheet("Выберите марку", "Отмена", null, brandNames);
        if (string.IsNullOrEmpty(selected) || selected == "Отмена") return;

        var targetBrand = brands.FirstOrDefault(b => b.Name == selected);
        if (targetBrand is null) return;

        await _mediator.Send(new MoveCarAdCommand(CarAd.Id, targetBrand.Id));
        
        WeakReferenceMessenger.Default.Send(new CarAdChangedMessage(0));
        await Shell.Current.DisplayAlert("Готово", $"Перемещено в \"{selected}\"", "OK");
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task PickImage()
    {
        if (CarAd is null) return;

        try
        {
            var photo = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions { Title = "Выберите фото" });
            if (photo is null) return;

            CarImage = null;
            await Task.Delay(100);

            Directory.CreateDirectory(ImagesFolder);
            var destPath = Path.Combine(ImagesFolder, $"{CarAd.Id}.jpg");

            using (var sourceStream = await photo.OpenReadAsync())
            {
                using (var destStream = File.Create(destPath))
                {
                    await sourceStream.CopyToAsync(destStream);
                    await destStream.FlushAsync();
                }
            }

            WeakReferenceMessenger.Default.Send(new CarAdChangedMessage(CarAd.Id));
            await Shell.Current.DisplayAlert("Готово", "Фото сохранено", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }
}