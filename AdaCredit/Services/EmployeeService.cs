using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Dtos;
using AdaCredit.Entities;
using AdaCredit.Persistence;
using AdaCredit.UseCases;

namespace AdaCredit.Services
{
    public class EmployeeService
    {
        private static EmployeeRepository _employeeRepository = new EmployeeRepository();

        public void AddNewEmployee(string name, string username)
        {
            if (!IsUsernameValid(username))
                throw new Exception("O username não está disponível");

            if (String.IsNullOrEmpty(name))
                throw new Exception("O nome não pode ser vazio");

            if (String.IsNullOrEmpty(username))
                throw new Exception("O username não pode ser vazio");

            var newEmployee = new Employee(name, username);

            _employeeRepository.AddNewEmployee(newEmployee);
            _employeeRepository.Save();
        }

        public void UpdatePassword(string password)
        {
            if (String.IsNullOrEmpty(password))
                throw new Exception("Senha não pode ser nula");

            Login.LoggedInUser.UpdatePassword(password);
            _employeeRepository.Save();
        }

        public bool DeleteEmployee(string username)
        {
            var employee =_employeeRepository.GetEmployeeByUsername(username);

            if (employee == null)
                throw new Exception("Funcionário não existe");             

            if (!employee.Active)
                throw new Exception("Funcionário já desativado");

            employee.Disable();
            _employeeRepository.Save();

            return true;
        }

        public bool IsUsernameValid(string username)
        {
            var existingEmployee = _employeeRepository.GetEmployeeByUsername(username);

            if (existingEmployee != null)
                return false;

            return true;
        }

        public List<EmployeeDto> GetAllActiveEmployees()
        {
            var activeEmployeesDto = new List<EmployeeDto>();
            var activeEmployees = _employeeRepository.GetAllActiveEmployees();

            foreach (var employee in activeEmployees)
            {
                activeEmployeesDto.Add(new EmployeeDto
                {
                    Name = employee.Name,
                    Username = employee.Username
                });
            }
            
            return activeEmployeesDto;
        }
    }
}