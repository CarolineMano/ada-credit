using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.Dtos
{
    public class EmployeeDto : PersonDto
    {
        public string? Username { get; set; }
    }
}