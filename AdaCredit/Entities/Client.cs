using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.Entities
{
    public sealed class Client : Person
    {
        public long Document { get; private set; }
        public Account Account { get; set; }

        public Client(string name, long document) : base(name)
        {
            Document = document;
            Account = new Account();
        }
    }
}