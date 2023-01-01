using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Enum;

namespace AdaCredit.Entities
{
    public sealed class Client : Person
    {
        public string Document { get; private set; }
        public Account Account { get; set; }
        public string Email { get; set; }
        public Decimal Balance { get; private set; }

        public Client(string name, string document, string email) : base(name)
        {
            Document = document;
            Account = new Account();
            Email = email;
            Balance = 0M;
        }

        public Client(string name, string document, string accountNumber, bool active, string email, decimal balance) : base(name, active)
        {
            Document = document;
            Account = new Account(accountNumber);
            Email = email;
            // Balance = Decimal.Parse(balance, NumberStyles.AllowDecimalPoint);
            Balance = balance;
        }

        public bool UpdateBalance(decimal value, TransactionFlow transactionFlow)
        {
            if (transactionFlow == TransactionFlow.Deposit)
            {
                Balance += value;
                return true;
            }

            if (Balance < value)
                return false;

            Balance -= value;
            return true;
        }
    }
}