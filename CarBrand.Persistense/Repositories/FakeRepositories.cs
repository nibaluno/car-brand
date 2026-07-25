namespace CarBrand.Persistense.Repositories;

using System.Linq.Expressions;

internal class FakeCarBrandRepository : IRepository<Domain.CarBrand>
{
    private readonly List<Domain.CarBrand> _list;

    public FakeCarBrandRepository()
    {
        _list = new List<Domain.CarBrand>();

        var brand1 = new Domain.CarBrand("Toyota", "Япония");
        brand1.Id = 1;
        _list.Add(brand1);

        var brand2 = new Domain.CarBrand("BMW", "Германия");
        brand2.Id = 2;
        _list.Add(brand2);
    }

    public async Task<IReadOnlyList<Domain.CarBrand>> ListAllAsync(
        CancellationToken cancellationToken = default)
        => await Task.FromResult(_list);

 
    public Task<Domain.CarBrand> GetByIdAsync(int id, CancellationToken ct = default,
        params Expression<Func<Domain.CarBrand, object>>[]? includes)
        => Task.FromResult(_list.FirstOrDefault(b => b.Id == id));

    public Task<IReadOnlyList<Domain.CarBrand>> ListAsync(
        Expression<Func<Domain.CarBrand, bool>> filter, CancellationToken ct = default,
        params Expression<Func<Domain.CarBrand, object>>[]? includes)
        => Task.FromResult<IReadOnlyList<Domain.CarBrand>>(
            _list.AsQueryable().Where(filter).ToList());

    public Task AddAsync(Domain.CarBrand entity, CancellationToken ct = default)
        => Task.CompletedTask;
    public Task UpdateAsync(Domain.CarBrand entity, CancellationToken ct = default)
        => Task.CompletedTask;
    public Task DeleteAsync(Domain.CarBrand entity, CancellationToken ct = default)
        => Task.CompletedTask;
    public Task<Domain.CarBrand> FirstOrDefaultAsync(
        Expression<Func<Domain.CarBrand, bool>> filter, CancellationToken ct = default)
        => Task.FromResult(_list.AsQueryable().FirstOrDefault(filter));
}

internal class FakeCarAdRepository : IRepository<CarAd>
{
    private readonly List<CarAd> _list;

    public FakeCarAdRepository()
    {
        _list = new List<CarAd>();
        var rnd = new Random();

        for (int brandId = 1; brandId <= 2; brandId++)
        {
            for (int j = 0; j < 5; j++)
            {
                var ad = new CarAd(
                    title: $"Объявление {j + 1}",
                    price: rnd.Next(5000, 50000),
                    year: rnd.Next(2010, 2024),
                    mileage: rnd.Next(0, 200000),
                    color: j % 2 == 0 ? "Белый" : "Чёрный");
                ad.AddToBrand(brandId);
                _list.Add(ad);
            }
        }
    }

    public async Task<IReadOnlyList<CarAd>> ListAsync(
        Expression<Func<CarAd, bool>> filter,
        CancellationToken ct = default,
        params Expression<Func<CarAd, object>>[]? includes)
        => await Task.FromResult(_list.AsQueryable().Where(filter).ToList());

    public async Task<IReadOnlyList<CarAd>> ListAllAsync(
        CancellationToken ct = default)
        => await Task.FromResult(_list);

    public Task<CarAd> GetByIdAsync(int id, CancellationToken ct = default,
        params Expression<Func<CarAd, object>>[]? includes)
        => Task.FromResult(_list.FirstOrDefault(a => a.Id == id));
    public Task AddAsync(CarAd entity, CancellationToken ct = default)
        => Task.CompletedTask;
    public Task UpdateAsync(CarAd entity, CancellationToken ct = default)
        => Task.CompletedTask;
    public Task DeleteAsync(CarAd entity, CancellationToken ct = default)
        => Task.CompletedTask;
    public Task<CarAd> FirstOrDefaultAsync(
        Expression<Func<CarAd, bool>> filter, CancellationToken ct = default)
        => Task.FromResult(_list.AsQueryable().FirstOrDefault(filter));
}