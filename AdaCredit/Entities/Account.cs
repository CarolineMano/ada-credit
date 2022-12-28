using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;

namespace AdaCredit.Entities
{
    public sealed class Account
    {
        public string Number { get; private set; }
        public string Branch { get; private set; }

        public Account()
        {
            Number = new Faker().Random.ReplaceNumbers("#####-#");
            Branch = "0001";
        }

        public Account(string accountNumber)
        {
            Number = accountNumber;
            Branch = "0001";
        }
    }
}