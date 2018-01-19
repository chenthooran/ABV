using System;
using System.Collections.Generic;
using System.Text;

namespace ABV.Core.Accounts.Queries.GetAccounts
{
    public class AccountsListItemModel
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public string Currency { get; set; }
    }
}
