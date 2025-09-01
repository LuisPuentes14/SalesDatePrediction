using Dapper;
using Microsoft.Data.SqlClient;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public  async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var sql = @"SELECT 
                        e.empid AS EmpId,
                        e.firstname AS FirstName ,
                        e.lastname AS  LastName
                    FROM HR.Employees e;";

            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<Employee>(sql);
            }
        }
    }
}
