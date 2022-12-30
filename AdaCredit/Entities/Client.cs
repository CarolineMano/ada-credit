using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.Entities
{
    public sealed class Client : Person
    {
        public string Document { get; private set; }
        public Account Account { get; set; }
        public string Email { get; set; }

        public Client(string name, string document, string email) : base(name)
        {
            Document = document;
            Account = new Account();
            Email = email;
        }

        public Client(string name, string document, string accountNumber, bool active, string email) : base(name, active)
        {
            Document = document;
            Account = new Account(accountNumber);
            Email = email;
        }
    }
}