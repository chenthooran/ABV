using ABV.Domain.Entities;

namespace ABV.Core.Accounts.Commands.CreateAccountBalance.Factory
{
    public class AccountBalanceFactory : IAccountBalanceFactory
    {
        public AccountBalance Create(Account account, Period period, decimal balance)
        {
            var accountBalance = new AccountBalance()
            {
                Account = account,
                Period = period,
                Balance = balance
            };
            return accountBalance;
        }
    }
}
