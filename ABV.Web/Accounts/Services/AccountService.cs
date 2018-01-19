using ABV.Core.Accounts.Commands.CreateAccount;
using ABV.Core.Accounts.Commands.CreateAccountBalance;
using ABV.Core.Accounts.Commands.CreatePeriod;
using ABV.Core.Accounts.Queries.GetAccountBalances;
using ABV.Core.Accounts.Queries.GetAccounts;
using ABV.Core.Accounts.Queries.GetPeriods;
using ABV.Core.Contracts;
using ABV.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABV.Web.Accounts.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGetAccountsListQuery _accountsQuery;
        private readonly IGetPeriodsListQuery _periodsQuery;
        private readonly IGetAccountBalancesListQuery _accountBalancesQuery;
        private readonly ICreateAccountCommand _createAccountCommand;
        private readonly ICreatePeriodCommand _createPeriodCommand;
        private readonly ICreateAccountBalanceCommand _createAccountBalanceCommand;

        public AccountService(IGetAccountsListQuery accountsQuery, IGetPeriodsListQuery periodsQuery, IGetAccountBalancesListQuery accountBalancesQuery,
            ICreateAccountCommand createAccountCommand, ICreatePeriodCommand createPeriodCommand, ICreateAccountBalanceCommand createAccountBalanceCommand)
        {
            _accountsQuery = accountsQuery;
            _periodsQuery = periodsQuery;
            _accountBalancesQuery = accountBalancesQuery;
            _createAccountCommand = createAccountCommand;
            _createPeriodCommand = createPeriodCommand;
            _createAccountBalanceCommand = createAccountBalanceCommand;
        }
        public async Task CreateAccountBalances(List<AccountBalanceViewModel> accountsVMList)
        {
            foreach (var item in accountsVMList)
            {
                var model = new CreateAccountModel() { Name = item.AccountName, Currency = "Rs" };
                _createAccountCommand.Execute(model);

                var account = _accountsQuery.Execute(item.AccountName);

                var periodModel = new CreatePeriodModel() { AsOfDate = item.AsOfDate };
                _createPeriodCommand.Execute(periodModel);

                var period = _periodsQuery.Execute(item.AsOfDate);

                var accBalanceModel = new CreateAccountBalanceModel()
                { AccountId = account.AccountId, PeriodId = period.PeriodId, Balance = item.AccountBalance };
                _createAccountBalanceCommand.Execute(accBalanceModel);
            }
        }

        public List<AccountBalancesListItemModel> GetLatestAccountBalances()
        {
            var balancesList = new List<AccountBalancesListItemModel>();
            var periods = _periodsQuery.Execute(top: 1);
            if(periods.Count != 0)
            {
                var date = periods[0].AsOfDate;
                balancesList = _accountBalancesQuery.Execute(date);
            }

            return balancesList;
        }

        public List<AccountBalanceChartModel> GetAccountwithBalances()
        {
            return _accountBalancesQuery.Execute();
        }

        public IEnumerable<string> GetAllDates()
        {
            var periods = _periodsQuery.Execute();
            return periods.Select(p => p.AsOfDate.ToString("dd/MM/yyyy"));
        }
    }
}
