namespace CarBrand.Application.UseCases.CarAds.Commands;

public sealed record MoveCarAdCommand(
    int AdId,
    int NewBrandId) : IRequest;