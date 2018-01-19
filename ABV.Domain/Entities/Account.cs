using System;
using System.Collections.Generic;
using System.Text;

namespace ABV.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public ICollection<AccountBalance> Balances { get; set; }
    }
}
