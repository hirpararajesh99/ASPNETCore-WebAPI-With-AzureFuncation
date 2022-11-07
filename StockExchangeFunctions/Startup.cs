
using fieldflake_coreflow.Services.GloContextRepository;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stockexchange.Model.StockExchangeDB;
using Stockexchange.Service.StockExchangeRepository.Implementation;
using Stockexchange.Service.StockExchangeRepository.Interface;
using System;

[assembly: FunctionsStartup(typeof(StockExchangeFunctions.Startup))]
namespace StockExchangeFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = ConfigHelper.GetConfig();

            #region "Set DB Connection"
            string connectionstring = config["DefaultConnection"];
            try
            {
                builder.Services.AddDbContext<StockExchangeContext>((serviceProvider, options) =>
            {

                options.UseSqlServer(connectionstring,
                                               sqlServerOptionsAction: sqlOptions =>
                                               {
                                                   sqlOptions.EnableRetryOnFailure();
                                               });
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging(true);
            });
                builder.Services.AddScoped<IUnitofWorkRepository, UnitofWorkRepository<StockExchangeContext>>();
                builder.Services.AddScoped<IStockDetailRepository, StockDetailRepository>();
                builder.Services.AddScoped<IQueueService, QueueService>();
            }
            catch (Exception)
            {

                // throw;
            }

            #endregion
        }

    }
}


