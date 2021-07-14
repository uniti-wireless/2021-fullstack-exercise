using CustomerManagerAPI.Configuration;
using CustomerManagerAPI.Data;
using CustomerManagerAPI.Helpers;
using CustomerManagerAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController : ControllerBase
    {
        public CustomerDBContext customerContext;

        public CustomersController()
        {
            // Intialize customer DB context to get the customer data from the yaml file
            customerContext = new CustomerDBContext();
        }

        // This method gets customer data from the database (yaml file in this case.
        // Paging filter to implement cpaging for customer data.
        // tag to filter all the customers by a particular tag
        // numEmployeesFilter to filter the customers by min and max number of employees.
        [HttpGet]
        public IActionResult GetCustomers([FromQuery] PagingFilter pagingFilter, string tag, [FromQuery] NumEmployeesFilter numEmployeesFilter)
        {
            // Get customers data
            IEnumerable<Customer> customers = (IEnumerable<Customer>)customerContext.Customers;
            if (!string.IsNullOrEmpty(tag))
            {
                // filter customers by tag
                customers = customers.Where(n => n.Tags.Contains(tag));
            }

            if (numEmployeesFilter != null)
            {
                // filter customers by nin and max number of employees
                if (numEmployeesFilter.MinNumEmployees > 0)
                {
                    customers = customers.Where(n => n.NumberOfEmployees >= numEmployeesFilter.MinNumEmployees);
                }

                if (numEmployeesFilter.MaxNumEmployees > 0)
                {
                    customers = customers.Where(n => n.NumberOfEmployees <= numEmployeesFilter.MaxNumEmployees);
                }
            }

            if (customers.Count() > 0)
            {
                // convert customer data into paginated list
                PaginatedList<Customer> customersAsPaginatedList = PaginatedList<Customer>
                    .ToPaginatedList(customers.ToList(), pagingFilter.PageNumber, pagingFilter.PageSize);

                // Create metdata to store pages info
                var metadata = new
                {
                    customersAsPaginatedList.TotalCount,
                    customersAsPaginatedList.TotalPages,
                    customersAsPaginatedList.PageSize,
                    customersAsPaginatedList.CurrentPage,
                    customersAsPaginatedList.HasNext,
                    customersAsPaginatedList.HasPrevious
                };

                // Add header for paginated list data
                Response.Headers.Add("PaginatedList", JsonConvert.SerializeObject(metadata));
                //return customers to the user
                return Ok(customersAsPaginatedList);
            }
            // Return no content found error
            return NoContent();
        }
    }
}
