using ABV.Domain.Entities;
using System;

namespace ABV.Core.Accounts.Commands.CreatePeriod.Factory
{
    public interface IPeriodFactory
    {
        Period Create(DateTime AsofDate);
    }
}
