namespace CarBrand.Application.UseCases.CarBrands.Queries;

internal class GetAllCarBrandsRequestHandler : IRequestHandler<GetAllCarBrandsRequest, IEnumerable<Domain.CarBrand>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCarBrandsRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Domain.CarBrand>> Handle(
        GetAllCarBrandsRequest request, 
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.CarBrandRepository.ListAllAsync(cancellationToken);
    }
}