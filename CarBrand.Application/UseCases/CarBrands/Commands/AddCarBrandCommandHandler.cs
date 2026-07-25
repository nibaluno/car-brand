namespace CarBrand.Application.UseCases.CarBrands.Commands;

internal class AddCarBrandCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddCarBrandCommand, Domain.CarBrand>
{
    public async Task<Domain.CarBrand> Handle(
        AddCarBrandCommand request,
        CancellationToken cancellationToken)
    {
        var brand = new Domain.CarBrand(request.Name, request.Country);
        await unitOfWork.CarBrandRepository.AddAsync(brand, cancellationToken);
        await unitOfWork.SaveAllAsync();
        return brand;
    }
}