namespace CarBrand.Domain.Abstractions;


//содержит набор репозиториев и ряд некоторых общих для них функций.
public interface IUnitOfWork
{
    IRepository<CarBrand> CarBrandRepository { get; }
    IRepository<CarAd> CarAdRepository { get; }

    Task SaveAllAsync();
    Task CreateDataBaseAsync();
    Task DeleteDataBaseAsync();
}