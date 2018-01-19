using ABV.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABV.Core.Accounts.Queries.GetPeriods
{
    public class GetPeriodsListQuery : IGetPeriodsListQuery
    {
        private readonly IDbContextService _dbService;

        public GetPeriodsListQuery(IDbContextService dbService)
        {
            _dbService = dbService;
        }

        public PeriodListItemModel Execute(DateTime asOfDate)
        {
            var items = _dbService.Periods.Where(p => p.AsofDate == asOfDate)
                .Select(a => new PeriodListItemModel()
                {
                    PeriodId = a.Id,
                    AsOfDate = a.AsofDate
                }).ToList();
            return items?.Count > 0 ? items[0] : null;
        }

        public List<PeriodListItemModel> Execute(int top = 0)
        {
            if(top == 0)
            {
                var allItems = _dbService.Periods.OrderBy(p => p.AsofDate)
                .Select(a => new PeriodListItemModel()
                {
                    PeriodId = a.Id,
                    AsOfDate = a.AsofDate
                });
                return allItems.ToList();
            }

            var items = _dbService.Periods.OrderByDescending(p => p.AsofDate).Take(top)
                .Select(a => new PeriodListItemModel()
                {
                    PeriodId = a.Id,
                    AsOfDate = a.AsofDate
                });
            return items.ToList();
        }

    }
}
