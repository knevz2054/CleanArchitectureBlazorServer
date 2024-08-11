using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Common.Enums
{
    public enum AccountType : byte
    {
        Cheque = 0,
        Current = 1,
        Savings = 2
    }
    public static class AccountTypeExtensions
    {
        public static string ToFriendlyName(this AccountType name)
        {
            switch (name)
            {
                case AccountType.Cheque:
                    return "Cheque";
                case AccountType.Current:
                    return "Current";
                case AccountType.Savings:
                    return "Savings";               
                default:
                    throw new Exception("Value is invalid");
            }
        }
    }

    public enum TransactionType : byte
    {
        Deposit = 0,
        Withdrawal = 1
    }

    public static class TransactionTypeExtensions
    {
        public static string ToFriendlyName(this TransactionType name)
        {
            switch (name)
            {
                case TransactionType.Deposit:
                    return "Deposit";
                case TransactionType.Withdrawal:
                    return "Current";                
                default:
                    throw new Exception("Value is invalid");
            }
        }
    }
}