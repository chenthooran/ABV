using ABV.Core.Accounts.Queries.GetAccountBalances;
using ABV.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABV.Core.Contracts
{
    public interface IAccountService
    {
        Task CreateAccountBalances(List<AccountBalanceViewModel> accountsVMList);
        List<AccountBalancesListItemModel> GetLatestAccountBalances();
        List<AccountBalanceChartModel> GetAccountwithBalances();
        IEnumerable<string> GetAllDates();
    }
}
