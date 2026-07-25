namespace CarBrand.Application.UseCases.CarAds.Queries;

public sealed record GetCarAdsByBrandRequest(int Id) : IRequest<IEnumerable<CarAd>>;