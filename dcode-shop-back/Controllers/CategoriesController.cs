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
using dcode_shop_back.Filters;
using dcode_shop_back.Helpers;
using dcode_shop_back.Services;

namespace dcode_shop_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ShopContext _context;
        private ICategoryService _categoryService;

        public CategoriesController(ShopContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id , [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize,filter.SortBy,filter.SortDirection, filter.brands,filter.priceLow,filter.priceHigh,filter.inStock );
            var pagedReponse = await _categoryService.GetCategoryById(id, filter, validFilter, route);

            if (pagedReponse == null)
            {
                return NotFound();
            }
            return Ok(pagedReponse);
        }
        // GET: api/Categories/5
        //[Authorize]
        [HttpGet("Recent/{id}")]
        public async Task<ActionResult<List<int>>> GetRecentCategory(int id)
        {
            
            var Reponse = await _categoryService.GetRecentCategoriesAsync(id);

            if (Reponse == null)
            {
                return NotFound();
            }
            return Ok(Reponse);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            var isSuperAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isSuperAdmin").Value);
            if (isAdmin || isSuperAdmin)
            {
                _context.Entry(category).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return Unauthorized();
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            var isSuperAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isSuperAdmin").Value);
            if (isAdmin || isSuperAdmin)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            else
            {
                return Unauthorized();
            }
            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
     
    }
}
