using System;
using System.Collections.Generic;
using System.Text;

namespace ABV.Core.Accounts.Queries.GetPeriods
{
    public interface IGetPeriodsListQuery
    {
        PeriodListItemModel Execute(DateTime asOfDate);
        List<PeriodListItemModel> Execute(int top = 0);
    }
}
