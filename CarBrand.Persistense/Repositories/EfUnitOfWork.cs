namespace CarBrand.Persistense.Repositories;

using CarBrand.Persistense.Data;

internal class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private readonly Lazy<IRepository<Domain.CarBrand>> _carBrandRepository;
    private readonly Lazy<IRepository<CarAd>> _carAdRepository;

    public EfUnitOfWork(AppDbContext context)
    {
        _context = context;
//Реальный объект EfRepository создастся только тогда, когда какая-то часть программы впервые обратится к свойству и напишет:
//unitOfWork.CarBrandRepository
        _carBrandRepository = new Lazy<IRepository<Domain.CarBrand>>(
            () => new EfRepository<Domain.CarBrand>(context));

        _carAdRepository = new Lazy<IRepository<CarAd>>(
            () => new EfRepository<CarAd>(context));
    }

    public IRepository<Domain.CarBrand> CarBrandRepository
        => _carBrandRepository.Value;

    public IRepository<CarAd> CarAdRepository
        => _carAdRepository.Value;

    public async Task SaveAllAsync()
        => await _context.SaveChangesAsync();

    public async Task CreateDataBaseAsync()
        => await _context.Database.EnsureCreatedAsync();

    public async Task DeleteDataBaseAsync()
        => await _context.Database.EnsureDeletedAsync();
}