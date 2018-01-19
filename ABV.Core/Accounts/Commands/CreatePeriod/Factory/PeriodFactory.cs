using ABV.Domain.Entities;
using System;

namespace ABV.Core.Accounts.Commands.CreatePeriod.Factory
{
    public class PeriodFactory : IPeriodFactory
    {
        public Period Create(DateTime asofDate)
        {
            var period = new Period() { AsofDate = asofDate };
            return period;
        }
    }
}
