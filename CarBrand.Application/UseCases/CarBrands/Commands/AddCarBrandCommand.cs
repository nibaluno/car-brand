namespace CarBrand.Application.UseCases.CarBrands.Commands;

public sealed record AddCarBrandCommand(
    string Name,
    string Country) : IRequest<Domain.CarBrand>;