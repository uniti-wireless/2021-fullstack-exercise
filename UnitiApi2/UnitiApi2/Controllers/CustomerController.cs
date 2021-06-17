using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitiApi2.Models;
using UnitiApi2.Repositories;

namespace UnitiApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _customerRepository.Get();
        }


        [HttpPost]
        public async Task<ActionResult<Customer>>PostCustomer([FromBody] Customer customer)
        {
            var newCustomer = await _customerRepository.Create(customer);
            return CreatedAtAction(nameof(GetCustomers), new { id = newCustomer.Id }, newCustomer);
        }

    }
}
