namespace CarBrand.Application.UseCases.CarBrands.Queries;

public sealed record GetAllCarBrandsRequest() : IRequest<IEnumerable<Domain.CarBrand>>;