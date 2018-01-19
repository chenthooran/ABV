using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;

namespace ABV.Context.Extensions
{
    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this ApplicationDbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void SeedInitialData(this ApplicationDbContext dbContext)
        {
            SeedAccounts(dbContext);
        }

        private static void SeedAccounts(ApplicationDbContext dbContext)
        {
            if (dbContext.Accounts.Any())
                return;
            dbContext.Accounts.Add(new Domain.Entities.Account { Name = "R & D", Currency = "Rs " });
            dbContext.Accounts.Add(new Domain.Entities.Account { Name = "Canteen", Currency = "Rs " });
            dbContext.Accounts.Add(new Domain.Entities.Account { Name = "CEO's Car", Currency = "Rs " });
            dbContext.Accounts.Add(new Domain.Entities.Account { Name = "Marketing", Currency = "Rs " });
            dbContext.Accounts.Add(new Domain.Entities.Account { Name = "Parking Fines", Currency = "Rs " });

            dbContext.SaveChanges();
        }
    }
}
