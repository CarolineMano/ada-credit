using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Entities;
using CsvHelper.Configuration;

namespace AdaCredit.Mapper
{
    public class ClientMap : ClassMap<Client>
    {
        public ClientMap()
        {
            Map(m => m.Name).Name("name");
            Map(m => m.Active).Name("active");
            Map(m => m.Document).Name("document");
            Map(m => m.Account.Number).Name("accountNumber");
            Map(m => m.Email).Name("email");
            Map(m => m.Balance).Name("balance");
        }
    }
}