using System;
using System.Collections.Generic;
using System.Text;

namespace ABV.Core.Accounts.Queries.GetAccountBalances
{
    public interface IGetAccountBalancesListQuery
    {
        List<AccountBalancesListItemModel> Execute(DateTime asOfDate);
        List<AccountBalanceChartModel> Execute();
    }
}
