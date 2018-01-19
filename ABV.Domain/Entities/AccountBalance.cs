using System;
using System.Collections.Generic;
using System.Text;

namespace ABV.Domain.Entities
{
    public class AccountBalance
    {
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid PeriodId { get; set; }
        public virtual Period Period { get; set; }
        public decimal Balance { get; set; }
    }
}
