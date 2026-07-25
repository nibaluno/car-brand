namespace CarBrand.Application.UseCases.CarAds.Commands;

internal class MoveCarAdCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MoveCarAdCommand>
{
    public async Task Handle(
        MoveCarAdCommand request,
        CancellationToken cancellationToken)
    {
        var ad = await unitOfWork.CarAdRepository
            .GetByIdAsync(request.AdId, cancellationToken);
        if (ad == null) return;

        ad.AddToBrand(request.NewBrandId);

        await unitOfWork.CarAdRepository.UpdateAsync(ad, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}