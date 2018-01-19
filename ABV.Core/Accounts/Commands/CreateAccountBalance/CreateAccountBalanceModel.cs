using System;

namespace ABV.Core.Accounts.Commands.CreateAccountBalance
{
    public class CreateAccountBalanceModel
    {
        public Guid AccountId { get; set; }
        public Guid PeriodId { get; set; }
        public decimal Balance { get; set; }
    }
}
