using System;

namespace ABV.Core.Accounts.Queries.GetPeriods
{
    public class PeriodListItemModel
    {
        public Guid PeriodId { get; set; }
        public DateTime AsOfDate { get; set; }
    }
}
