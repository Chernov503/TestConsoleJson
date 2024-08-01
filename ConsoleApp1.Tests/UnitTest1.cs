using ConsoleApp1.Application;
using ConsoleApp1.Application.Models;
using ConsoleApp1.DTOs;
using Newtonsoft.Json;

namespace ConsoleApp1.Tests
{
    public class UnitTest1
    {
        private const string JSON_NAME = "employees_test.json";

        [Fact]
        public void Test1()
        {
            #region Arrange
            if (File.Exists(JSON_NAME))
                File.Delete(JSON_NAME);
            
            var employeeService = new EmployeeService(JSON_NAME);

            var request = new AddEmployeeRequest
            {
                FirstName = "First",
                LastName = "Last",
                SalaryPerHour = 100.2m
            };
            #endregion            

            #region Act
            employeeService.AddEmployee(request);
            #endregion            

            #region Assert

            var jsonData = File.ReadAllText(JSON_NAME);
            var employees = JsonConvert.DeserializeObject<List<Employee>>(jsonData);

            Assert.NotNull(employees);
            Assert.Single(employees);

            var employee = employees[0];
            Assert.Equal("First", employee.FirstName);
            Assert.Equal("Last", employee.LastName);
            Assert.Equal(100.2m, employee.SalaryPerHour);

            #endregion
        }
    }
}