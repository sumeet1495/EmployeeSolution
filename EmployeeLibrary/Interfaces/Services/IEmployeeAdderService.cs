using EmployeeLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLibrary.Interfaces.Services
{
    public interface IEmployeeAdderService
    {
        Task<int> AddEmployeeAsync(CreateEmployeeDto dto);
    }
}

