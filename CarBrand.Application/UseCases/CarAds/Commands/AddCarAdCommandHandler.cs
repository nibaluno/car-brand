namespace CarBrand.Application.UseCases.CarAds.Commands;

internal class AddCarAdCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddCarAdCommand, CarAd>
{
    public async Task<CarAd> Handle(
        AddCarAdCommand request,
        CancellationToken cancellationToken)
    {
        var ad = new CarAd(
            request.Title,
            request.Price,
            request.Year,
            request.Mileage,
            request.Color);

        ad.AddToBrand(request.BrandId);

        await unitOfWork.CarAdRepository.AddAsync(ad, cancellationToken);
        await unitOfWork.SaveAllAsync();
        return ad;
    }
}