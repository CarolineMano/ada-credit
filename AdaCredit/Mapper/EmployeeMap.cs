using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Entities;
using CsvHelper.Configuration;

namespace AdaCredit.Mapper
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Map(m => m.Name).Name("name");
            Map(m => m.Username).Name("username");
            Map(m => m.Salt).Name("salt");
            Map(m => m.LastLoggedIn).Name("lastLoggedIn");
            Map(m => m.Active).Name("active");
            Map(m => m.PasswordHash).Name("passwordHash");
            Map(m => m.FirstLogin).Name("firstLogin");
        }
    }
}