using System;
using System.Collections.Generic;
using System.Text;

namespace ABV.Core.Accounts.Queries.GetAccountBalances
{
    public class AccountBalanceChartModel
    {
        public string Label { get; set; }
        public IEnumerable<Decimal> Data { get; set; }
    }
}
