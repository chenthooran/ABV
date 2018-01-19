using ABV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABV.Core.Accounts.Commands.CreateAccount.Factory
{
    public interface IAccountFactory
    {
        Account Create(string name, string currency);
    }
}
