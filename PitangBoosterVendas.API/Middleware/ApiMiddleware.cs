using log4net;
using Microsoft.AspNetCore.Http.Features;
using PitangBoosterVendas.Repository;
using PitangBoosterVendas.Utils.Attributes;
using System.Diagnostics;
using System.Net;
using System.Transactions;

namespace PitangBoosterVendas.API.Middleware
{
    public class ApiMiddleware(ITransactionManager transactionManager) : IMiddleware
    {
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var transactionRequired = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata.GetMetadata<TransactionRequiredAttribute>();
            try
            {

                if (transactionRequired != null)
                {
                    await _transactionManager.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);

                    await next.Invoke(context);

                    await _transactionManager.CommitTransactionAsync();
                }
                else
                {
                    await next.Invoke(context);
                }


                stopwatch.Stop();
            }
            catch (Exception ex)
            {
                if (transactionRequired != null)
                {
                    await _transactionManager.RollbackTransactionsAsync();
                }

                stopwatch.Stop();
            }
        }
    }
}
