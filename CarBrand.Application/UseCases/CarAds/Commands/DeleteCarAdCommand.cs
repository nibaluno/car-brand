namespace CarBrand.Application.UseCases.CarAds.Commands;

public sealed record DeleteCarAdCommand(int Id) : IRequest;