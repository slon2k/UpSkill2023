namespace Northwind.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();

    TEntity? GetById(int id);

    void Update(TEntity entity);
    
    void Delete(TEntity entity);

    void Create(TEntity entity);

    void SaveChanges();
}
