using EmployeeLibrary.Dtos;
using EmployeeLibrary.Interfaces.Repositories;
using EmployeeLibrary.Interfaces.Services;
using EmployeeLibrary.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeLibrary.Services
{
    public class EmployeeAdderService : IEmployeeAdderService
    {
        private readonly IEmployeeAdderRepository _repository;
        private readonly ILogger<EmployeeAdderService> _logger;

        public EmployeeAdderService(IEmployeeAdderRepository repository, ILogger<EmployeeAdderService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<int> AddEmployeeAsync(CreateEmployeeDto dto)
        {
            try
            {
                var employee = new Employee
                {
                    EmpName = dto.EmpName,
                    Salary = dto.Salary
                };

                var result = await _repository.AddEmployeeAsync(employee);

                _logger.LogInformation("Employee added successfully: {EmpName}", employee.EmpName);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add employee: {EmpName}", dto.EmpName);
                return -1;
            }
        }

    }
}
