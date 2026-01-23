using Demo02.Data;
using Demo02.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Demo02.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Customer> GetActiveCustomers()
        {
            // Call Stored Procedure using FromSqlRaw
            return _context.Customers
                .FromSqlRaw("EXECUTE dbo.GetActiveCustomers")
                .ToList();
        }

        public void UpdateCustomerStatus(int id, string newStatus)
        {
            // Call Stored Procedure using ExecuteSqlRaw with SqlParameters
            var pId = new SqlParameter("@CustomerId", id);
            var pStatus = new SqlParameter("@NewStatus", newStatus);

            _context.Database.ExecuteSqlRaw("EXECUTE dbo.UpdateCustomer @CustomerId, @NewStatus", pId, pStatus);
        }
    }
}
