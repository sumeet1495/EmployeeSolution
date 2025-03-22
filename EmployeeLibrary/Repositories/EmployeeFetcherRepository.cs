using EmployeeLibrary.Interfaces.Database;
using EmployeeLibrary.Interfaces.Repositories;
using EmployeeLibrary.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace EmployeeLibrary.Repositories
{
    public class EmployeeFetcherRepository : IEmployeeFetcherRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly ILogger<EmployeeFetcherRepository> _logger;

        public EmployeeFetcherRepository(IDbConnectionFactory dbFactory, ILogger<EmployeeFetcherRepository> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();
                string sql = "SELECT emp_id, emp_name, salary FROM sales.employee";
                var employees = (await connection.QueryAsync<Employee>(sql)).ToList();

                _logger.LogInformation("Fetched {Count} employees successfully.", employees.Count);

                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fetch failed.");
                return new List<Employee>();
            }
        }
    }
}

