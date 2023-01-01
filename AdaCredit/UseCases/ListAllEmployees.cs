using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class ListAllEmployees
    {
        private static EmployeeService _employeeService = new EmployeeService();

        public static void Show()
        {
            try
            {
                Console.Clear();

                Console.WriteLine($"***Funcionários***{Environment.NewLine}");

                var employees = _employeeService.GetAllActiveEmployees();

                foreach (var employee in employees)
                {
                    Console.WriteLine("********************");
                    Console.WriteLine(employee);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}