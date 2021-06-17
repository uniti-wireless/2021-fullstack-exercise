using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitiApi2.Models;

namespace UnitiApi2.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> Get();
        Task<Customer> Get(string id);
        Task<Customer> Create(Customer customer);

    }
}
