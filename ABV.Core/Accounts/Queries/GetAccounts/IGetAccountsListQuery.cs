using System.Collections.Generic;

namespace ABV.Core.Accounts.Queries.GetAccounts
{
    public interface IGetAccountsListQuery
    {
        AccountsListItemModel Execute(string name);
        //List<AccountsListItemModel> Execute();
    }
}
