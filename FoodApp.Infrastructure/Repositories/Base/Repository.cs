using FoodApp.Domain.Entities;
using FoodApp.Domain.Interface.Base;
using FoodApp.Infrastructure.Data;
using System.Linq.Expressions;

namespace FoodApp.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Add(entity);
            return entity;
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().Where(e => !e.Deleted);
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }
        public T GetByID(int id)
        {
            return GetAll().FirstOrDefault(t => t.ID == id);
        }
        public T First(Expression<Func<T, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Any(predicate);
        }
        public void Update(T entity)
        {
            _context.Update(entity);
        }
        public void Delete(int id)
        {
            var entity = GetByID(id);
            Delete(entity);
        }
        public void Delete(T entity)
        {
            entity.Deleted = true;
            Update(entity);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
       

    }
}
