using System;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Process
    {

        public static ProcessResult Run(Action action)
        {
            try
            {
                action();
                return ProcessResult.Ok();
            }
            catch (InvalidOperationException ie)
            {
                return ProcessResult.Fail(ie.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<ProcessResult> RunAsync(Func<Task> action)
        {
            try
            {
                await action();
                return ProcessResult.Ok();
            }
            catch (InvalidOperationException ie)
            {
                return ProcessResult.Fail(ie.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static ProcessResult<T> Run<T>(Func<T> action)
        {
            try
            {
                var result = action();
                return ProcessResult<T>.Ok(result);
            }
            catch (InvalidOperationException ie)
            {
                return ProcessResult<T>.Fail(ie.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<ProcessResult<T>> RunAsync<T>(Func<Task<T>> action)
        {
            try
            {
                var result = await action();
                return ProcessResult<T>.Ok(result);
            }
            catch (InvalidOperationException ie)
            {
                return ProcessResult<T>.Fail(ie.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<ProcessResult> RunInTransactionAsync(Func<Task> action, ShoppingCartContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    await action();
                    transaction.Commit();
                    return ProcessResult.Ok();
                }
                catch (InvalidOperationException ie)
                {
                    transaction.Rollback();
                    return ProcessResult.Fail(ie.Message);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }   
            }
        }

        public static async Task<ProcessResult<T>> RunInTransactionAsync<T>(Func<Task<T>> action, ShoppingCartContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var result = await action();
                    transaction.Commit();
                    return ProcessResult<T>.Ok(result);
                }
                catch (InvalidOperationException ie)
                {
                    transaction.Rollback();
                    return ProcessResult<T>.Fail(ie.Message);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
