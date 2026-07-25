namespace CarBrand.Application.UseCases.CarAds.Commands;

public sealed record AddCarAdCommand(
    string Title,
    decimal Price,
    int Year,
    int Mileage,
    string Color,
    int BrandId) : IRequest<CarAd>;