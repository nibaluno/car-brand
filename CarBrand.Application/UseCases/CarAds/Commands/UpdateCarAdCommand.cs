namespace CarBrand.Application.UseCases.CarAds.Commands;

public sealed record UpdateCarAdCommand(
    int Id,
    string Title,
    decimal Price,
    int Year,
    int Mileage,
    string Color) : IRequest;