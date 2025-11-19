namespace Lab1.Core.Interfaces;

public abstract class ARepository<T> : IRepository<T> where T : IEntity
{
    protected readonly Dictionary<Guid, T> Storage = new();
    
    public virtual T? GetById(Guid id)
    {
        Storage.TryGetValue(id, out var entity);
        return entity;
    }
    
    public virtual IEnumerable<T> GetAll() => Storage.Values.ToList();
    
    public virtual void Add(T entity) => Storage[entity.Id] = entity;

    public virtual void Remove(Guid id) => Storage.Remove(id);
}