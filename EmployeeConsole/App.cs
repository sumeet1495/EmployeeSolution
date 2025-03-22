using EmployeeLibrary.Dtos;
using EmployeeLibrary.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EmployeeConsole
{
    public class App
    {
        private readonly IEmployeeAdderService _adderService;
        private readonly IEmployeeFetcherService _fetcherService;
        private readonly ILogger<App> _logger;

        public App(IEmployeeAdderService adderService, IEmployeeFetcherService fetcherService, ILogger<App> logger)
        {
            _adderService = adderService;
            _fetcherService = fetcherService;
            _logger = logger;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.WriteLine("\n===== Employee Management System =====");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. View All Employees");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");
                var choice = Console.ReadLine();
                //Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        await AddEmployeeAsync();
                        break;
                    case "2":
                        await FetchAndDisplayEmployeesAsync();
                        break;
                    case "3":
                        Console.WriteLine("Exiting application...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private async Task AddEmployeeAsync()
        {
            try
            {
                Console.Write("Enter Employee Name: ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    _logger.LogError("Employee name is required.");
                    return;
                }

                Console.Write("Enter Salary: ");
                string salaryInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(salaryInput) || !decimal.TryParse(salaryInput, out var salary)) // out - outputing and storing in salary variable
                {
                    _logger.LogError("Invalid or missing salary input.");
                    return;
                }

                var dto = new CreateEmployeeDto { EmpName = name, Salary = salary };

                var context = new ValidationContext(dto); // data annotation - means validating the dto against required and range 
                var results = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(dto, context, results, true);

                if (!isValid)
                {
                    foreach (var result in results)
                        Console.WriteLine(result.ErrorMessage);
                    return;
                }

                int empId = await _adderService.AddEmployeeAsync(dto);
                if (empId > 0)
                {
                    _logger.LogInformation($"Employee added. ID: {empId}");
                }
                else
                {
                    _logger.LogError("Failed to add employee.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the employee.");
                Console.WriteLine("Something went wrong while adding the employee. Please try again later.");
            }
        }

        private async Task FetchAndDisplayEmployeesAsync()
        {
            Console.WriteLine("\nAll Employees:\n");

            try
            {
                var employees = await _fetcherService.GetAllEmployeesAsync();

                if (employees.Count == 0)
                {
                    Console.WriteLine("No employees found.");
                    return;
                }

                foreach (var emp in employees)
                {
                    Console.WriteLine($"ID: {emp.EmpId}, Name: {emp.EmpName}, Salary: {emp.Salary}");
                }

                _logger.LogInformation("Fetched and displayed all employees successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch employees.");
                Console.WriteLine("An error occurred while fetching employee records. Please try again later.");
            }
        }
    }
}
