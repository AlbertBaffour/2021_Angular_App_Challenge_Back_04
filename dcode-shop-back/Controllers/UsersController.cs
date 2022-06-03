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
using dcode_shop_back.Services;

namespace dcode_shop_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "https://dcode-shop-back-api.azurewebsites.net", headers: "*", methods: "*")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly ShopContext _context;

        public UsersController(ShopContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User userParam)
        {
            var user = _userService.Authenticate(userParam.Email, userParam.Password);
            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });
            return Ok(user);
        }
        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var isSuperAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isSuperAdmin").Value);
            if (isSuperAdmin)
            {
                return await _context.Users.Include(x=>x.Customer).ToListAsync();
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET: api/Users/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Include(x=>x.Customer).SingleOrDefaultAsync(u=>u.Id==id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var isSuperAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isSuperAdmin").Value);
            if (isSuperAdmin)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                return Unauthorized();
            }
            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
