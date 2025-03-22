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
    public class EmployeeAdderRepository : IEmployeeAdderRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly ILogger<EmployeeAdderRepository> _logger;

        public EmployeeAdderRepository(IDbConnectionFactory dbFactory, ILogger<EmployeeAdderRepository> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();
                string sql = @"INSERT INTO sales.employee (emp_name, salary)
                               VALUES (@EmpName, @Salary)
                               RETURNING emp_id;";
                var result = await connection.ExecuteScalarAsync<int>(sql, employee);

                _logger.LogInformation("Employee inserted successfully. EmpName: {EmpName}, Salary: {Salary}, Generated ID: {EmpId}",
                                       employee.EmpName, employee.Salary, result);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert failed.");
                return -1;
            }
        }
    }
}

