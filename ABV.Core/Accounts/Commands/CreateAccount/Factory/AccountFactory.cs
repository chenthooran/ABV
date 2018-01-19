using ABV.Domain.Entities;

namespace ABV.Core.Accounts.Commands.CreateAccount.Factory
{
    public class AccountFactory : IAccountFactory
    {
        public Account Create(string name, string currency)
        {
            var account = new Account()
            {
                Name = name,
                Currency = currency
            };
            return account;
        }
    }
}
