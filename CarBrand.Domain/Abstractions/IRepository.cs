namespace CarBrand.Domain.Abstractions;

using System.Linq.Expressions;
// CRUD-интерфейс
//чтобы привести все технологии работы с БД к общему знаменателю, мы можем реализовать паттерн репозиторий.
public interface IRepository<T> where T : Entity
{
    /// <summary>
    /// Поиск сущности по Id
    /// </summary>
    Task<T> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includesProperties);

    /// <summary>
    /// Получение всего списка сущностей
    /// </summary>
    Task<IReadOnlyList<T>> ListAllAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение отфильтрованного списка
    /// </summary>
    Task<IReadOnlyList<T>> ListAsync(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includesProperties);

    /// <summary>
    /// Добавление новой сущности
    /// </summary>
    Task AddAsync(
        T entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Изменение сущности
    /// </summary>
    Task UpdateAsync(
        T entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    Task DeleteAsync(
        T entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Поиск первой сущности, удовлетворяющей условию.
    /// Если не найдена — возвращает значение по умолчанию.
    /// </summary>
    Task<T> FirstOrDefaultAsync(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default);
}