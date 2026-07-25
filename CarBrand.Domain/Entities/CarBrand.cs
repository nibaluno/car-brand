namespace CarBrand.Domain;

/// <summary>
/// Марка автомобиля — агрегат-корень.
/// Управляет коллекцией объявлений о продаже.
/// </summary>
public class CarBrand : Entity
{
    private List<CarAd> _ads = new();

  
    private CarBrand() { }

    public CarBrand(string name, string country)
    {
        Name = name;
        Country = country;
    }

    /// <summary>Название марки (Toyota, BMW и т.д.)</summary>
    public string Name { get; set; }

    /// <summary>Страна-производитель — дополнительное свойство группы</summary>
    public string Country { get; private set; }

    /// <summary>Список объявлений (только для чтения снаружи)</summary>
    public IReadOnlyList<CarAd> Ads => _ads.AsReadOnly();

    /// <summary>Добавить объявление к этой марке</summary>
    public void AddAd(CarAd ad)
    {
        if (ad == null) return;
        _ads.Add(ad);
    }

    /// <summary>Убрать объявление</summary>
    public void RemoveAd(CarAd ad)
    {
        _ads.Remove(ad);
    }
}