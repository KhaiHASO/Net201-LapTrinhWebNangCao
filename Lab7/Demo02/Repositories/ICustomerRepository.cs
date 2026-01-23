using Demo02.Models;
using System.Collections.Generic;

namespace Demo02.Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetActiveCustomers();
        void UpdateCustomerStatus(int id, string newStatus);
    }
}
