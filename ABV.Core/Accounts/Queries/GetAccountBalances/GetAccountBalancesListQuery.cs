using ABV.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABV.Core.Accounts.Queries.GetAccountBalances
{
    public class GetAccountBalancesListQuery : IGetAccountBalancesListQuery
    {
        private readonly IDbContextService _dbService;

        public GetAccountBalancesListQuery(IDbContextService dbService)
        {
            _dbService = dbService;
        }

        public List<AccountBalancesListItemModel> Execute(DateTime asOfDate)
        {
            var items = _dbService.AccountBalances.Where(a => a.Period.AsofDate == asOfDate) //.Include(a => a.Period)
                .Select(a => new AccountBalancesListItemModel()
                {
                    AsOfDate = a.Period.AsofDate,
                    AccountName = a.Account.Name,
                    Balance = a.Balance,
                    Currency = a.Account.Currency
                }).ToList();
            return items;
        }

        public List<AccountBalanceChartModel> Execute()
        {
            return (from a in _dbService.Accounts.Include(a => a.Balances)
                    select new AccountBalanceChartModel
                    {
                        Label = a.Name,
                        Data = a.Balances.OrderBy(b => b.Period.AsofDate).Select(b => b.Balance)
                    }).ToList();
        }
    }
}
