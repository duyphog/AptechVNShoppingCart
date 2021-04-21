using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public ShoppingCartContext AppContext { get; set; }

        public RepositoryBase(ShoppingCartContext context)
        {
            AppContext = context;
        }

        public IQueryable<T> FindAll()
        {
            return AppContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return AppContext.Set<T>().Where(expression);
        }

        public void Create(T entity)
        {
            AppContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            AppContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            AppContext.Set<T>().Remove(entity);
        }

        public void AddRange(List<T> entities)
        {
            AppContext.AddRange(entities);
        }

        //public void SetNewValueEntry(T entityOld, T entityNew)
        //{
        //    AppContext.Entry(entityOld).CurrentValues.SetValues(entityNew);
        //}
    }
}
