using TestApp.Core.Services.Banking;
using TestApp.DomainModel.Context;
using TestApp.Repository;
using TestApp.Repository.BankAccount;
using TestApp.Repository.Transaction;
using TestApp.Utils;
using TestApp.Utils.HttpReponse;
using TestApp.Web.Initializers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TestApp.Web
{
    public class Startup
    {

        #region "Private Member(s)"
        private ILoggerFactory _loggerFactory;
        #endregion

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            _loggerFactory = loggerFactory;

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("PostgresSqlProvider");

            //Add entity framework and db context
            services.AddEntityFramework();

            services.AddEntityFrameworkNpgsql();

            services.AddDbContext<TestAppTestDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Add framework services.
            services.AddMvc()
                .AddMvcOptions(options => options.Filters.Add(new GlobleExceptionFilter(_loggerFactory)));

            services.AddAuthentication();
            services.AddDistributedMemoryCache();
            services.AddSession();

            //Register application repositories
            services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            //Register application services
            services.AddScoped<IBankingService, BankingService>();
            services.AddScoped<IHttpResponseResult, HttpResponseResult>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();

            //Initialize database
            DbInitializer.Initialize(app);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "dashboard",
                template: "dashboard",
                defaults:new { controller = "BankAccount", action = "Index" });

                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Authentication", action = "Login" });
            });
        }
    }
}
