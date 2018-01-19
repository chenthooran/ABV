using System;
using System.Collections.Generic;
using System.Text;

namespace ABV.Domain.Entities
{
    public class Period : BaseEntity
    {
        public DateTime AsofDate { get; set; }
        public ICollection<AccountBalance> Balances { get; set; }
    }
}
