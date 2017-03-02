using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Utils.enums;

namespace TestApp.Core.ViewModels
{
    public class TransactionVM
    {
        public int Number { get; set; }
        public double Amount { get; set; }
        public string Perticulars { get; set; }
        public bool IsDebit { get; set; }
        public double Balance { get; set; }
    }
}
