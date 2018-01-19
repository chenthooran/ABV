namespace ABV.Core.Accounts.Commands.CreateAccountBalance
{
    public interface ICreateAccountBalanceCommand
    {
        void Execute(CreateAccountBalanceModel model);
    }
}
