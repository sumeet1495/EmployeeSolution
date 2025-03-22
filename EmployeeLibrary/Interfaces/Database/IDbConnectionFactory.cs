using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeLibrary.Interfaces.Database
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}

