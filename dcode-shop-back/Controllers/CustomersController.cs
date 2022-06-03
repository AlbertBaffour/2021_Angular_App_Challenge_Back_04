using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dcode_shop_back.Data;
using dcode_shop_back.Models;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Authorization;

namespace dcode_shop_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "https://dcode-shop-back-api.azurewebsites.net", headers: "*", methods: "*")]
    public class CustomersController : ControllerBase
    {
        private readonly ShopContext _context;

        public CustomersController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            var isSuperAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isSuperAdmin").Value);
            if (isAdmin || isSuperAdmin)
            {
                return await _context.Customers.ToListAsync();
            }
            else
            {
                return Unauthorized();
            }
            return NoContent();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var existingUser = _context.Users.Where(c => c.Email == customer.Email).FirstOrDefault();
            if (existingUser == null)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
            }
            return Conflict();
        }

        // DELETE: api/Customers/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            var isSuperAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isSuperAdmin").Value);
            if (isAdmin || isSuperAdmin)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                return Unauthorized();
            }

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
