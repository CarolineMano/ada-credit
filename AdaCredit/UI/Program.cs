using System;
using System.Globalization;
using AdaCredit.UseCases;

namespace AdaCredit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HomePage.Show();
            Login.Show();
            Menu.Show(args);
        }
    }
}