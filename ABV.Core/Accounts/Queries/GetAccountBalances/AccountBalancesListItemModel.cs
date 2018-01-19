using System;
using System.Collections.Generic;
using System.Text;

namespace ABV.Core.Accounts.Queries.GetAccountBalances
{
    public class AccountBalancesListItemModel
    {
        public string AccountName { get; set; }
        public DateTime AsOfDate { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
    }
}
