using ABV.Core.Accounts.Commands.CreateAccount.Factory;
using ABV.Core.Contracts;
using System.Linq;

namespace ABV.Core.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand : ICreateAccountCommand
    {
        private readonly IAccountFactory _factory;
        private readonly IDbContextService _dbService;

        public CreateAccountCommand(IAccountFactory factory, IDbContextService dbService)
        {
            _factory = factory;
            _dbService = dbService;
        }

        public void Execute(CreateAccountModel model)
        {
            var account = _dbService.Accounts.FirstOrDefault(a => a.Name == model.Name);

            if(account == null)
            {
                account = _factory.Create(model.Name, model.Currency);

                _dbService.Accounts.Add(account);
                _dbService.Save();
            }
        }
    }
}
