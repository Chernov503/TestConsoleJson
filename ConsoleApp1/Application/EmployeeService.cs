using ConsoleApp1.Application.Models;
using ConsoleApp1.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp1.Application
{
    //Можно было бы сделать через MediatR
    public class EmployeeService
    {
        #region Props
        private readonly string _filePath;

        private List<Employee> employees;
        #endregion

        #region Ctor
        public EmployeeService(string filePath)
        {
            _filePath = filePath;
            employees = LoadEmployees();
        }
        #endregion

        #region Methods
        private List<Employee> LoadEmployees()
        {
            if (!File.Exists(_filePath))
                return [];

            using (var reader = File.OpenText(_filePath))
            {
                var json = reader.ReadToEnd();
                var employees = JsonConvert.DeserializeObject<List<Employee>>(json);
                return employees ?? [];
            }
        }

        private void SaveEmployees()
        {
            var json = JsonConvert.SerializeObject(employees, Newtonsoft.Json.Formatting.Indented);

            using (var writer = new StreamWriter(_filePath))
            {
                writer.Write(json);
            }
        }

        public void AddEmployee(AddEmployeeRequest employee)
        {
            var maxId = employees.Any() ? employees.Max(e => e.Id) : 0;

            employees.Add(new Employee
            {
                Id = ++maxId,
                FirstName = employee.FirstName!,
                LastName = employee.LastName!,
                SalaryPerHour = employee.SalaryPerHour!.Value
            });

            SaveEmployees();
        }

        public void UpdateEmployee(UpdateEmployeeRequest employeeNew)
        {
            var employeeOld = employees.SingleOrDefault(e => e.Id == employeeNew.Id);
            if (employeeOld == null)
            {
                Console.WriteLine("Пользователь не найден");
                return;
            }

            employeeOld.FirstName = string.IsNullOrEmpty(employeeNew.FirstName) ? employeeOld.FirstName : employeeNew.FirstName;
            employeeOld.LastName = string.IsNullOrEmpty(employeeNew.LastName) ? employeeOld.LastName : employeeNew.LastName;
            employeeOld.SalaryPerHour = !employeeNew.SalaryPerHour.HasValue ? employeeOld.SalaryPerHour : employeeNew.SalaryPerHour.Value;

            SaveEmployees();
        }

        public void GetEmployee(int id)
        {
            var employee = employees.SingleOrDefault(x => x.Id == id);
            if (employee == null)
            {
                Console.WriteLine($"Пользователь с Id:{id} не найден");
                return;
            }

            //Тут мог быть автомаппер
            var result = new GetEmployeeResponse
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                SalaryPerHour = employee.SalaryPerHour,
            };

            Console.WriteLine(result);
        }

        public void DeleteEmployee(int id)
        {
            var employee = employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                Console.WriteLine($"Пользователь с Id:{id} не найден");
                return;
            }

            employees.Remove(employee);
            SaveEmployees();
        }

        public void GetAllEmployees()
        {
            //тут мог быть автомаппер
            foreach (var employee in employees)
            {
                var result = new GetEmployeeResponse
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    SalaryPerHour = employee.SalaryPerHour,
                };
                Console.WriteLine(result);
            }
        }

        #endregion
    }
}
