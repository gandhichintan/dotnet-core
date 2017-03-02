using TestApp.DomainModel.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TestApp.DomainModel.Entity;
using TestApp.Repository.BankAccount;
using System.Threading.Tasks;

namespace TestApp.Web.Initializers
{
    /// <summary>
    /// Database initializer class
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Initialize database
        /// Ensure it has been created to insert default application data
        /// </summary>
        /// <param name="app"></param>
        public static void Initialize(IApplicationBuilder app)
        {
            // Get an instance of the DbContext from the DI container
            using (var context = app.ApplicationServices.GetRequiredService<TestAppTestDbContext>())
            {
                if (context.Database.EnsureCreated())
                {
                    var bankAccountRepository = app.ApplicationServices.GetRequiredService<IBankAccountRepository>();

                    var testaccount = new BankAccount
                    {
                        UserName = "testaccount@bank.com",
                        Password = "Test@ccount1"
                    };
                    Task.Run(() => bankAccountRepository.Create(testaccount))
                        .Wait();

                    var demoaccount = new BankAccount
                    {
                        UserName = "demoaccount@bank.com",
                        Password = "Demo@ccount1"
                    };
                    Task.Run(() => bankAccountRepository.Create(demoaccount))
                        .Wait();
                }
            }
        }
    }
}
