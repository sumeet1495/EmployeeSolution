using EmployeeLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeLibrary.Interfaces.Repositories
{
    public interface IEmployeeFetcherRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync();
    }
}

