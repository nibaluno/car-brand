# CarBrand — мобильное приложение объявлений об автомобилях

Кроссплатформенное приложение на **.NET MAUI** для управления марками автомобилей и объявлениями о продаже. Проект демонстрирует применение **Clean Architecture**, **CQRS** и **DDD** в мобильной разработке.

## О проекте

CarBrand — учебный pet-проект, реализующий каталог автомобильных объявлений с группировкой по маркам. Пользователь может просматривать марки, создавать объявления, редактировать и удалять их, перемещать между марками и прикреплять фотографии.

Приложение работает офлайн: данные хранятся локально в **SQLite**, изображения — в файловой системе устройства.

## Возможности

- Просмотр списка марок автомобилей и объявлений по выбранной марке
- CRUD-операции: добавление марки, создание / редактирование / удаление объявления
- Перемещение объявления между марками
- Прикрепление фото к объявлению через галерею устройства
- Цветовая индикация цены (value converter)
- Реактивное обновление UI через `WeakReferenceMessenger`

## Архитектура

```
CarBrand/
├── CarBrand.Domain          # Сущности, интерфейсы репозиториев
├── CarBrand.Application     # Use Cases (MediatR: Commands & Queries)
├── CarBrand.Persistense     # EF Core, SQLite, репозитории
└── CarBrand.Mobile          # .NET MAUI UI (MVVM)
```

| Слой | Ответственность |
|------|-----------------|
| **Domain** | `CarBrand`, `CarAd` — агрегаты с бизнес-логикой (валидация цены, пробега, привязка к марке) |
| **Application** | CQRS через MediatR: `AddCarAdCommand`, `GetCarAdsByBrandRequest` и др. |
| **Persistence** | `AppDbContext`, `EfRepository`, `EfUnitOfWork`, инициализация БД |
| **Mobile** | XAML-страницы, ViewModels (CommunityToolkit.Mvvm), DI, навигация Shell |

### Паттерны и подходы

- **Clean Architecture** — зависимости направлены к Domain
- **CQRS** — разделение команд и запросов (MediatR)
- **Repository + Unit of Work**
- **MVVM** — `ObservableObject`, `[RelayCommand]`, data binding
- **DDD** — `CarBrand` как aggregate root, связь 1:N с `CarAd`

## Стек технологий

- **.NET 8** / **.NET MAUI**
- **Entity Framework Core 8** + SQLite
- **MediatR** — медиатор для use cases
- **CommunityToolkit.Maui** / **CommunityToolkit.Mvvm**
- **Dependency Injection** (`Microsoft.Extensions.DependencyInjection`)

## Платформы

- Android (API 21+)
- iOS (11+)
- macOS (Mac Catalyst 13.1+)
- Windows (10.0.17763+)

## Запуск

### Требования

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Workload .NET MAUI](https://learn.microsoft.com/dotnet/maui/get-started/installation)

```bash
dotnet workload install maui
```

### Сборка и запуск

```bash
git clone <repository-url>
cd CarBrand

dotnet restore CarBrand.sln
dotnet build CarBrand.sln

# Android
dotnet build CarBrand.Mobile/CarBrand.Mobile.csproj -f net8.0-android
dotnet build CarBrand.Mobile/CarBrand.Mobile.csproj -t:Run -f net8.0-android

# iOS Simulator (macOS)
dotnet build CarBrand.Mobile/CarBrand.Mobile.csproj -t:Run -f net8.0-ios

# Mac Catalyst
dotnet build CarBrand.Mobile/CarBrand.Mobile.csproj -t:Run -f net8.0-maccatalyst
```

> При первом запуске база данных создаётся автоматически и заполняется тестовыми данными (Toyota, BMW, Tesla).

## Структура экранов

| Экран | Описание |
|-------|----------|
| `CarBrandsPage` | Главный экран: выбор марки, список объявлений |
| `CarAdDetailsPage` | Детали объявления, фото, удаление, перемещение |
| `AddCarBrandPage` | Добавление новой марки |
| `AddCarAdPage` | Создание объявления для выбранной марки |
| `EditCarAdPage` | Редактирование объявления |

