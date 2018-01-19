using ABV.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABV.Core.Accounts.Queries.GetAccounts
{
    public class GetAccountsListQuery : IGetAccountsListQuery
    {
        private readonly IDbContextService _dbService;

        public GetAccountsListQuery(IDbContextService dbService)
        {
            _dbService = dbService;
        }
        public AccountsListItemModel Execute(string name)
        {
            var items = _dbService.Accounts.Where(a => a.Name == name)
                .Select(a => new AccountsListItemModel() {
                    AccountId = a.Id,
                    AccountName = a.Name,
                    Currency = a.Currency
                }).ToList();

            return items?.Count > 0 ? items[0] : null;
        }
    }
}
