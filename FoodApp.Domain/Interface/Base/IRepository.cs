﻿using FoodApp.Domain.Entities;
using System.Linq.Expressions;

namespace FoodApp.Domain.Interface.Base
{
    public interface IRepository<T> where T : BaseModel
    {
        T Add(T entity);
        IQueryable<T> GetAll();
        IQueryable<TResult> GetAllWithProjection<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T GetByID(int id);
        TResult GetByIDWithProjection<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);
        T First(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Delete(int id);
        void Delete(T entity);
        void SaveChanges();
    }
}
