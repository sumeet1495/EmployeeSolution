using EmployeeLibrary.Interfaces.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Npgsql;


namespace EmployeeLibrary.Database
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        private readonly ILogger<DbConnectionFactory> _logger;

        public DbConnectionFactory(string connectionString, ILogger<DbConnectionFactory> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public IDbConnection CreateConnection()
        {
            try
            {
                return new NpgsqlConnection(_connectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create DB connection.");
                throw;
            }
        }
    }
}

