using System;
using System.Collections.Generic;
using System.Text;

namespace ABV.Core.ViewModels
{
    public class AccountBalanceViewModel
    {
        public string AccountName { get; set; }
        public decimal AccountBalance { get; set; }
        public DateTime AsOfDate { get; set; }
    }
}
