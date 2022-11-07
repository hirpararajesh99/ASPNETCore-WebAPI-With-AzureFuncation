using Stockexchangeapi.Helper;

namespace Stockexchangeapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region "Set DB Connection"
            string connectionstring = Configuration["DefaultConnection"];


            services.AddDbContext<StockExchangeContext>((serviceProvider, options) =>
            {

                options.UseSqlServer(connectionstring,
                                               sqlServerOptionsAction: sqlOptions =>
                                               {
                                                   sqlOptions.EnableRetryOnFailure();
                                               });
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging(true);
            });
            services.AddScoped<IUnitofWorkRepository, UnitofWorkRepository<StockExchangeContext>>();
            services.AddScoped<IStockDetailRepository, StockDetailRepository>();
            #endregion

            services.AddMvc(options =>
            {

            })
            .AddMvcOptions(o => o.AllowEmptyInputInBodyModelBinding = true);

            services.AddSwaggerGen();

            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddControllersWithViews().AddDataAnnotationsLocalization();
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpStatusCodeExceptionMiddleware();

            app.UseHttpsRedirection();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            //// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            //// specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock Exchange API V1");
            });
        }

    }
}
