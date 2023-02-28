using Northwind.Data;

namespace Northwind.Repositories;

public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly NorthwindContext context;

    public RepositoryBase(NorthwindContext context)
    {
        this.context = context;
    }

    public virtual void Create(TEntity entity) => context.Add(entity);

    public virtual void Delete(TEntity entity) => context.Remove(entity);

    public virtual IEnumerable<TEntity> GetAll() => context.Set<TEntity>().ToList();

    public virtual TEntity? GetById(int id) => context.Find<TEntity>(id);

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public virtual void Update(TEntity entity) => context.Update(entity);
}
