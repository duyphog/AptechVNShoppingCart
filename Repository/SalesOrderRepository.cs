using System;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class SalesOrderRepository : RepositoryBase<SalesOrder>, ISalesOrderRepository
    {
        public SalesOrderRepository(ShoppingCartContext context) : base(context)
        {
        }

        public int GetNewOrderNumberFromSequence()
        {
            var param = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            AppContext.Database.ExecuteSqlRaw("set @result = next value for ordernumber_seq", param);
            return (int)param.Value;
        }
    }
}
