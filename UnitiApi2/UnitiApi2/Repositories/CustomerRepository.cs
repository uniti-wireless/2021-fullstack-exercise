using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitiApi2.Models;

namespace UnitiApi2.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        




        public async Task<Customer> Create(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<Customer>> Get()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> Get(string id)
        {
            return await _context.Customers.FindAsync();
        }
    }
}
