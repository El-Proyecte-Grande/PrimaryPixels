using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Repositories;

public abstract class Repository<T> : IRepository<T>
{
    public abstract IEnumerable<T> GetAll();
    public abstract T GetById(int id);
    public abstract int? Add(T entity);
    public abstract int Update(T entity);
    public abstract void DeleteById(int id);
}