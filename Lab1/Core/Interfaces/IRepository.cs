namespace Lab1.Core.Interfaces;

public interface IRepository<T> where T : IEntity
{
    public T? GetById(Guid id);
    public IEnumerable<T> GetAll();
    public void Add(T entity);
    public void Remove(Guid id);
}