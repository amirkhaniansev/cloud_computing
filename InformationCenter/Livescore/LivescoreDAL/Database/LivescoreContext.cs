using System.Threading.Tasks; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using LivescoreDAL.Configurations;
using LivescoreDAL.Parameters;

namespace LivescoreDAL.Database
{
    public class LivescoreContext : DbContext
    {
        private readonly bool isTest;
        private readonly string databaseName;
        private readonly string connectionString;

        public LivescoreContext(DatabaseConfiguration configuration) :
            this(configuration.IsTest, configuration.DatabaseName, configuration.ConnectionString)
        {
        }

        public LivescoreContext(bool isTest, string databaseName, string connectionString)
        {
            this.isTest = isTest;
            this.databaseName = databaseName;
            this.connectionString = connectionString;
        }

        public async Task<bool> Save()
        {
            var transaction = default(IDbContextTransaction);
            var database = this.Database;

            try
            {
                await this.SaveChangesAsync();
                if (transaction != null)
                    await transaction.CommitAsync();

                return true;
            }
            catch
            {
                if (transaction != null)
                    await transaction.RollbackAsync();

                return false;
            }
            finally
            {
                if (transaction != null)
                    await transaction.DisposeAsync();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (this.isTest)
                optionsBuilder.UseInMemoryDatabase(this.databaseName);
            else optionsBuilder.UseSqlServer(this.connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompetitionConfiguration())
                        .ApplyConfiguration(new CountryConfiguration())
                        .ApplyConfiguration(new EventConfiguration())
                        .ApplyConfiguration(new MatchConfiguration())
                        .ApplyConfiguration(new PlayerConfiguration())
                        .ApplyConfiguration(new SeasonConfiguration())
                        .ApplyConfiguration(new SeasonTeamConfiguration())
                        .ApplyConfiguration(new SportConfiguration())
                        .ApplyConfiguration(new TeamConfiguration());
        }
    }
}