using ABV.Core.Accounts.Commands.CreatePeriod.Factory;
using ABV.Core.Contracts;
using System.Linq;

namespace ABV.Core.Accounts.Commands.CreatePeriod
{
    public class CreatePeriodCommand : ICreatePeriodCommand
    {
        private readonly IPeriodFactory _factory;
        private readonly IDbContextService _dbService;

        public CreatePeriodCommand(IPeriodFactory factory, IDbContextService dbService)
        {
            _factory = factory;
            _dbService = dbService;
        }

        public void Execute(CreatePeriodModel model)
        {
            var period = _dbService.Periods.FirstOrDefault(a => a.AsofDate == model.AsOfDate);

            if (period == null)
            {
                period = _factory.Create(model.AsOfDate);
                _dbService.Periods.Add(period);
                _dbService.Save();
            }
        }
    }
}
