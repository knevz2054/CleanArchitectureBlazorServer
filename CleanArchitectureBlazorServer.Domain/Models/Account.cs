using CleanArchitectureBlazorServer.Common.Contracts;
using CleanArchitectureBlazorServer.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Common.Models
{
    public class Account : BaseEntity<int>
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public AccountType Type { get; set; }

        public int AccountHolderId { get; set; }

        public AccountHolder AccountHolder { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
