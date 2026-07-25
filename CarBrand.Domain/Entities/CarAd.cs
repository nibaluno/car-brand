namespace CarBrand.Domain;

/// <summary>
/// Объявление о продаже автомобиля.
/// </summary>
public class CarAd : Entity
{
    // Приватный конструктор для EF Core
    private CarAd() { }

    public CarAd(string title, decimal price, int year,
        int mileage, string color)
    {
        Title = title;
        Price = price;    
        Year = year;
        Mileage = mileage;
        Color = color;
    }

    /// <summary>Заголовок объявления</summary>
    public string Title { get; private set; }

    /// <summary>
    /// ★ Цена 
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>Год выпуска автомобиля</summary>
    public int Year { get; private set; }

    /// <summary>Пробег в км</summary>
    public int Mileage { get; private set; }

    /// <summary>Цвет кузова</summary>
    public string Color { get; private set; }

    /// <summary>FK — идентификатор марки</summary>
    public int? CarBrandId { get; private set; }
    
    public CarBrand? CarBrand { get; private set; }
    public void ChangeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) return;
        Title = title;
    }

    public void ChangeYear(int year)
    {
        if (year < 1900 || year > DateTime.Now.Year) return;
        Year = year;
    }

    public void ChangeMileage(int mileage)
    {
        if (mileage < 0) return;
        Mileage = mileage;
    }

    public void ChangeColor(string color)
    {
        if (string.IsNullOrWhiteSpace(color)) return;
        Color = color;
    }
    /// <summary>
    /// Бизнес-метод: изменить цену.
    /// Цена не может быть отрицательной.
    /// </summary>
    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 0) return;
        Price = newPrice;
    }

    /// <summary>Привязать объявление к марке</summary>
    public void AddToBrand(int brandId)
    {
        if (brandId <= 0) return;
        CarBrandId = brandId;
    }

    /// <summary>Открепить от марки</summary>
    public void LeaveBrand()
    {
        CarBrandId = null;
    }
}