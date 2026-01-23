using Demo02.Models;
using Demo02.Repositories;
using System.Collections.Generic;

namespace Demo02.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public List<Customer> GetActiveCustomers()
        {
            return _repository.GetActiveCustomers();
        }

        public void UpdateCustomerStatus(int id, string newStatus)
        {
            _repository.UpdateCustomerStatus(id, newStatus);
        }
    }
}
