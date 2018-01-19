using System;
using System.Collections.Generic;
using System.Text;

namespace ABV.Core.Accounts.Commands.CreateAccount
{
    public interface ICreateAccountCommand
    {
        void Execute(CreateAccountModel model);
    }
}
