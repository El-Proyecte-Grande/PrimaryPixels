namespace PrimaryPixels.Services.Repositories;

public abstract class Repository<T> : IRepository<T>
{
    public abstract Task<IEnumerable<T>> GetAll();
    public abstract Task<T> GetById(int id);
    public abstract Task<int> Add(T entity);
    public abstract Task<int> Update(T entity);
    public abstract Task<int> DeleteById(int id);
}