namespace CarBrand.Application.UseCases.CarAds.Queries;

internal class GetCarAdsByBrandRequestHandler : IRequestHandler<GetCarAdsByBrandRequest, IEnumerable<CarAd>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCarAdsByBrandRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CarAd>> Handle(
        GetCarAdsByBrandRequest request, 
        CancellationToken cancellationToken)
    {
        // Фильтруем объявления так, чтобы их CarBrandId совпадал с запрошенным Id
        return await _unitOfWork.CarAdRepository.ListAsync(
            a => a.CarBrandId == request.Id, 
            cancellationToken);
    }
}