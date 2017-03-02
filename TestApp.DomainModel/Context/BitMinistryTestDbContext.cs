using TestApp.DomainModel.Entity;
using Microsoft.EntityFrameworkCore;

namespace TestApp.DomainModel.Context
{
    /// <summary>
    /// Represents database context
    /// </summary>
    public class TestAppTestDbContext : DbContext
    {
        public TestAppTestDbContext(DbContextOptions<TestAppTestDbContext> options):base(options)
        {

        }

        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>()
                .HasMany<Transaction>();

            modelBuilder.Entity<Transaction>().HasOne<BankAccount>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
