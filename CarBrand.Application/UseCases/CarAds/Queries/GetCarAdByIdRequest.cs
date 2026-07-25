namespace CarBrand.Application.UseCases.CarAds.Queries;

public sealed record GetCarAdByIdRequest(int Id) : IRequest<CarAd?>;