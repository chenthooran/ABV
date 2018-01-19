using ABV.Core.Accounts.Commands.CreateAccountBalance.Factory;
using ABV.Core.Contracts;
using System.Linq;

namespace ABV.Core.Accounts.Commands.CreateAccountBalance
{
    public class CreateAccountBalanceCommand : ICreateAccountBalanceCommand
    {
        private readonly IAccountBalanceFactory _factory;
        private readonly IDbContextService _dbService;

        public CreateAccountBalanceCommand(IAccountBalanceFactory factory, IDbContextService dbService)
        {
            _factory = factory;
            _dbService = dbService;
        }

        public void Execute(CreateAccountBalanceModel model)
        {
            var account = _dbService.Accounts.SingleOrDefault(a => a.Id == model.AccountId);

            var period = _dbService.Periods.SingleOrDefault(p => p.Id == model.PeriodId);

            var accountBalance = _dbService.AccountBalances.FirstOrDefault(a => a.AccountId == model.AccountId && a.PeriodId == model.PeriodId);

            if (accountBalance == null)
            {
                 accountBalance = _factory.Create(account, period, model.Balance);
                _dbService.AccountBalances.Add(accountBalance);
            }
            else
            {
                accountBalance.Balance = model.Balance;
            }
            _dbService.Save();
        }
    }
}
