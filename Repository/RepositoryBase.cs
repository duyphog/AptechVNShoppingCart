using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Microsoft.Data.SqlClient;
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

        /// <summary>
        /// AdrangeAsync Entity to DataContext
        /// </summary>
        /// <param name="entities"></param>
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await AppContext.AddRangeAsync(entities);
        }

        /// <summary>
        /// Get next value for sequence name from database
        /// </summary>
        /// <param name="sequenceName"></param>
        /// <returns></returns>
        protected int GetNextValueForSequence(string sequenceName)
        {
            var param = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            AppContext.Database.ExecuteSqlRaw($"set @result = next value for {sequenceName}", param);
            return (int)param.Value;
        }
    }
}
