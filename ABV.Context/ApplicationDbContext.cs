using ABV.Core.Contracts;
using ABV.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ABV.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContextService
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<AccountBalance> AccountBalances { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AccountBalance>().HasKey(ps => new { ps.AccountId, ps.PeriodId });
            builder.Entity<AccountBalance>().HasOne(s => s.Account).WithMany(p => p.Balances).HasForeignKey(s => s.AccountId);
            builder.Entity<AccountBalance>().HasOne(p => p.Period).WithMany(s => s.Balances).HasForeignKey(p => p.PeriodId);
        }

        public void Save()
        {
            SaveChanges();
        }
    }
}
