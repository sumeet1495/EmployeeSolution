using EmployeeLibrary.Dtos;
using EmployeeLibrary.Interfaces.Repositories;
using EmployeeLibrary.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLibrary.Services
{
    public class EmployeeFetcherService : IEmployeeFetcherService
    {
        private readonly IEmployeeFetcherRepository _repository;
        private readonly ILogger<EmployeeFetcherService> _logger;

        public EmployeeFetcherService(IEmployeeFetcherRepository repository, ILogger<EmployeeFetcherService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _repository.GetAllEmployeesAsync();

                var employeeDtos = employees.Select(e => new EmployeeDto
                {
                    EmpId = e.EmpId,
                    EmpName = e.EmpName,
                    Salary = e.Salary
                }).ToList();

                _logger.LogInformation("Fetched {Count} employees successfully.", employeeDtos.Count);

                return employeeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch employees.");
                return new List<EmployeeDto>();
            }
        }
    }
}
