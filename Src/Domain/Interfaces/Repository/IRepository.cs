using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> AddAsync(T entity);
}