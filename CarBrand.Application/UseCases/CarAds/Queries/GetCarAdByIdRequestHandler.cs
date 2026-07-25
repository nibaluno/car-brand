namespace CarBrand.Application.UseCases.CarAds.Queries;

internal class GetCarAdByIdRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetCarAdByIdRequest, CarAd?>
{
    public async Task<CarAd?> Handle(
        GetCarAdByIdRequest request,
        CancellationToken cancellationToken)
    {
        return await unitOfWork.CarAdRepository
            .GetByIdAsync(request.Id, cancellationToken);
    }
}