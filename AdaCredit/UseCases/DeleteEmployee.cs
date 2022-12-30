using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Dtos;
using AdaCredit.Services;

namespace AdaCredit.UseCases
{
    public static class DeleteEmployee
    {
        public static EmployeeService _employeeService = new EmployeeService();

        public static void Show()
        {
            Console.Clear();

            var activeEmployees = _employeeService.GetAllActiveEmployees();
            ShowEmployees(activeEmployees);

            Console.WriteLine("Digite o username do funcionário que deseja deletar: ");

            var username = Console.ReadLine();

            _employeeService.DeleteEmployee(username);
        }

        public static void ShowEmployees(List<EmployeeDto> employeeDtos)
        {
            Console.WriteLine("****Funcionários ativos****");
            foreach (var employee in employeeDtos)
            {
                Console.WriteLine($"Nome: {employee.Name} | Username: {employee.Username}");
            }
        }
    }
}