namespace CarBrand.Persistense.Data;
//посредник между твоим кодом на C# и  SQL
internal class AppDbContext : DbContext
{
    public DbSet<Domain.CarBrand> CarBrands { get; set; } //набор DbSet для работы с текущей сущностью
    public DbSet<CarAd> CarAds { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Создаёт БД если её нет
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) //Fluent Builder
    {
        // CarAd не существует без CarBrand — настраиваем связь
        //Сутью данного паттерна является разбиение процесса создания объекта на несколько
        //этапов вместо создания одного универсального конструктора. 
        modelBuilder.Entity<CarAd>()
            .HasOne(a => a.CarBrand)
            .WithMany(b => b.Ads)
            .HasForeignKey(a => a.CarBrandId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}