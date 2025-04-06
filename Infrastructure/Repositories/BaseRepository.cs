using ApplicationCore.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> :IRepository<T> where T : class
{
    private readonly MovieDbContext _movieDbContext;
    
    public BaseRepository(MovieDbContext movieDbContext)
    {
        this._movieDbContext = movieDbContext;
    }
    
    public int Insert(T entity)
    {
        _movieDbContext.Set<T>().Add(entity);
        return _movieDbContext.SaveChanges();
    }

    public int Update(T entity)
    {
        _movieDbContext.Set<T>().Entry(entity).State = EntityState.Modified;
        return _movieDbContext.SaveChanges();
    }

    public int Delete(int id)
    {
        var item = GetById(id);
        if (item != null)
        {
            _movieDbContext.Set<T>().Remove(item);
            return _movieDbContext.SaveChanges();
        }
        return 0;
    }

    public IEnumerable<T> GetAll()
    {
        return _movieDbContext.Set<T>();
    }

    public T GetById(int id)
    {
        return _movieDbContext.Set<T>().Find(id);
    }
}