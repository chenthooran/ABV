using ABV.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABV.Core.Contracts
{
    public interface IDbContextService
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Period> Periods { get; set; }
        DbSet<AccountBalance> AccountBalances { get; set; }
        void Save();
    }
}
