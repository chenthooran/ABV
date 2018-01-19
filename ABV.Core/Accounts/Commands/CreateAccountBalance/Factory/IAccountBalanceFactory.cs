using ABV.Domain.Entities;

namespace ABV.Core.Accounts.Commands.CreateAccountBalance.Factory
{
    public interface IAccountBalanceFactory
    {
        AccountBalance Create(Account account, Period period, decimal balance);
    }
}
