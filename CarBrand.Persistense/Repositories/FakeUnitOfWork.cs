namespace CarBrand.Persistense.Repositories;

internal class FakeUnitOfWork : IUnitOfWork
{
    public IRepository<Domain.CarBrand> CarBrandRepository { get; }
        = new FakeCarBrandRepository();

    public IRepository<CarAd> CarAdRepository { get; }
        = new FakeCarAdRepository();

    public Task SaveAllAsync() => Task.CompletedTask;
    public Task CreateDataBaseAsync() => Task.CompletedTask;
    public Task DeleteDataBaseAsync() => Task.CompletedTask;
}