using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Entities;
using AdaCredit.Persistence;
using AdaCredit.UseCases;
using CsvHelper;

namespace AdaCredit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Login.Show();
            Menu.Show(args);
            // AddNewEmployee.Show();
        }
    }
}