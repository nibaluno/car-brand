using Microsoft.Extensions.DependencyInjection;

namespace CarBrand.Application;

//гарант что бд будет заполнена
//Инициализация происходит при первом обращении к контексту данных.
public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider services)
    {
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();
        
        await unitOfWork.DeleteDataBaseAsync();
        await unitOfWork.CreateDataBaseAsync();

       
        var toyota = new Domain.CarBrand("Toyota", "Япония");
        var bmw = new Domain.CarBrand("BMW", "Германия");
        var tesla = new Domain.CarBrand("Tesla", "США");

        await unitOfWork.CarBrandRepository.AddAsync(toyota);
        await unitOfWork.CarBrandRepository.AddAsync(bmw);
        await unitOfWork.CarBrandRepository.AddAsync(tesla);
        
        await unitOfWork.SaveAllAsync();

     
        var ads = new List<CarAd>
        {
            new CarAd("Toyota Camry 2021", 32000m, 2021, 35000, "Белый"),
            new CarAd("Toyota Corolla 2018", 18500m, 2018, 70000, "Серебристый"),
            new CarAd("Toyota Vitz (Битая)", 4500m, 2010, 210000, "Серый"),

            new CarAd("BMW M5 F90", 95000m, 2020, 15000, "Синий"),
            new CarAd("BMW X5 xDrive", 55000m, 2019, 48000, "Черный"),

            new CarAd("Tesla Model 3 Long Range", 42000m, 2022, 12000, "Красный")
        };

        ads[0].AddToBrand(toyota.Id);
        ads[1].AddToBrand(toyota.Id);
        ads[2].AddToBrand(toyota.Id); // 1:N (Один-ко-многим)

        ads[3].AddToBrand(bmw.Id);
        ads[4].AddToBrand(bmw.Id);

        ads[5].AddToBrand(tesla.Id);

        foreach (var ad in ads)
        {
            await unitOfWork.CarAdRepository.AddAsync(ad);
        }

        await unitOfWork.SaveAllAsync();
    }
}