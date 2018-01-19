using System;
using System.Collections.Generic;
using System.Text;

namespace ABV.Core.Accounts.Commands.CreatePeriod
{
    public interface ICreatePeriodCommand
    {
        void Execute(CreatePeriodModel model);
    }
}
