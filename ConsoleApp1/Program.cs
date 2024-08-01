using ConsoleApp1;
using ConsoleApp1.Application;
using ConsoleApp1.Application.Services;
using ConsoleApp1.DTOs;
using System.ComponentModel.DataAnnotations;

internal class Program
{
    static void Main(string[] args)
    {
        //args = ["-delete", "Id:5"];

        if (args.Length == 0)
        {
            Console.WriteLine("No arguments provided.");
            return;
        }
        foreach (string arg in args) { Console.WriteLine(arg); }

        var employeeService = new EmployeeService("employees.json");

        try
        {
            switch (args[0])
            {
                case "-add":
                    {
                        var employee = new AddEmployeeRequest();
                        employee.FirstName = TryGetValueFromArgs.GetStringByKey(args, nameof(employee.FirstName));
                        employee.LastName = TryGetValueFromArgs.GetStringByKey(args, nameof(employee.LastName));
                        employee.SalaryPerHour = TryGetValueFromArgs.GetDecimalByKey(args, nameof(employee.SalaryPerHour));

                        var isValid = ValidatorHelper.Validate(employee);
                        if (!isValid) break;

                        employeeService.AddEmployee(employee);
                        break;
                    }
                case "-update":
                    {
                        var employee = new UpdateEmployeeRequest();
                        employee.Id = TryGetValueFromArgs.GetIntByKey(args, nameof(employee.Id));
                        employee.FirstName = TryGetValueFromArgs.GetStringByKey(args, nameof(employee.FirstName));
                        employee.LastName = TryGetValueFromArgs.GetStringByKey(args, nameof(employee.LastName));
                        employee.SalaryPerHour = TryGetValueFromArgs.GetDecimalByKey(args, nameof(employee.SalaryPerHour));

                        var isValid = ValidatorHelper.Validate(employee);
                        if (!isValid) break;

                        employeeService.UpdateEmployee(employee);
                        break;
                    }
                case "-get":
                    {
                        var employeeId = TryGetValueFromArgs.GetIntByKey(args, "Id");
                        if (employeeId == null)
                        {
                            Console.WriteLine("некорректный Id");
                            break;
                        }

                        employeeService.GetEmployee(employeeId.Value);
                        break;
                    }
                case "-delete":
                    {
                        var employeeId = TryGetValueFromArgs.GetIntByKey(args, "Id");
                        if (employeeId == null)
                        {
                            Console.WriteLine("некорректный Id");
                            break;
                        }

                        employeeService.DeleteEmployee(employeeId.Value);
                        break;
                    }
                case "-getall":
                    {
                        employeeService.GetAllEmployees();
                        break;
                    }
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
        catch { Console.WriteLine("Unknown error"); }
    }
}