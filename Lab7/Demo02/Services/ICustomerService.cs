using Demo02.Models;
using System.Collections.Generic;

namespace Demo02.Services
{
    public interface ICustomerService
    {
        List<Customer> GetActiveCustomers();
        void UpdateCustomerStatus(int id, string newStatus);
    }
}
