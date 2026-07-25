namespace CarBrand.Application.UseCases.CarAds.Commands;

internal class DeleteCarAdCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCarAdCommand>
{
    public async Task Handle(
        DeleteCarAdCommand request,
        CancellationToken cancellationToken)
    {
        var ad = await unitOfWork.CarAdRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (ad == null) return;

        await unitOfWork.CarAdRepository.DeleteAsync(ad, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}