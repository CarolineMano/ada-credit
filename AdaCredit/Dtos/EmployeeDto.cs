using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Entities;

namespace AdaCredit.Dtos
{
    public class EmployeeDto : PersonDto
    {
        public string? Username { get; set; }
        public DateTime LastLoggedIn { get; set; }

        public EmployeeDto(Employee employee)
        {
            Name = employee.Name;
            Username = employee.Username;
            LastLoggedIn = employee.LastLoggedIn;
        }

        public override string ToString()
        {
            return $"Nome: {Name} {Environment.NewLine}Username: {Username}{Environment.NewLine}Último login: {LastLoggedIn}";
        }
    }
}