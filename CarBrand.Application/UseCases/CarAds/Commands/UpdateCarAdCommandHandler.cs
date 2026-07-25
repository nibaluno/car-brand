namespace CarBrand.Application.UseCases.CarAds.Commands;

internal class UpdateCarAdCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCarAdCommand>
{
    public async Task Handle(
        UpdateCarAdCommand request,
        CancellationToken cancellationToken)
    {
        var ad = await unitOfWork.CarAdRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (ad == null) return;

        ad.ChangeTitle(request.Title);
        ad.ChangePrice(request.Price);
        ad.ChangeYear(request.Year);
        ad.ChangeMileage(request.Mileage);
        ad.ChangeColor(request.Color);

        await unitOfWork.CarAdRepository.UpdateAsync(ad, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}