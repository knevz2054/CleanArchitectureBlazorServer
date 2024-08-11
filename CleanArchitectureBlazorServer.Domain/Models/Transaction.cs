using CleanArchitectureBlazorServer.Common.Contracts;
using CleanArchitectureBlazorServer.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Common.Models
{
    public class Transaction : BaseEntity<int>
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
