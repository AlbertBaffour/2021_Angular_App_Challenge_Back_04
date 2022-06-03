using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dcode_shop_back.Data;
using dcode_shop_back.Models;
using dcode_shop_back.Wrappers;
using dcode_shop_back.Filters;
using dcode_shop_back.Services;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Authorization;

namespace dcode_shop_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "https://dcode-shop-back-api.azurewebsites.net", headers: "*", methods: "*")]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;
        private IProductService _productService;

        public ProductsController(ShopContext context,IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize,null,null,filter.brands, filter.priceLow,filter.priceHigh,filter.inStock);
            var pagedReponse =await _productService.GetProducts(filter, validFilter,route);
            if (pagedReponse == null)
                return BadRequest(new { message = "something went wrong in ProductService" });
            return Ok(pagedReponse);
        }

        // GET: api/Products/All
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAll([FromQuery] PaginationFilter filter)
        {
            var pagedReponse = await _productService.GetProductsAll();
            if (pagedReponse == null)
                return BadRequest(new { message = "something went wrong in ProductService" });
            return Ok(pagedReponse);
        }

        // GET: api/Products/Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<String>>> GetProductsBrands()
        {
                     
            var response =await _productService.GetProductsBrands();
            response = new HashSet<string>(response).ToList();
            if (response == null)
                return BadRequest(new { message = "something went wrong in ProductService/brands" });
            return Ok(response);
        }

        // GET: api/ProductsAdmin 
        // Deze komt dus ook met inactieve producten
        [Authorize]
        [HttpGet("Admin")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAdmin()
        {
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            var isSuperAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isSuperAdmin").Value);
            if (isAdmin || isSuperAdmin)
            {
               
                var Reponse =await _productService.GetProductsAdmin();
                if (Reponse == null)
                    return BadRequest(new { message = "something went wrong in ProductService" });
                return Ok(Reponse);
            }
            return Unauthorized();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(new Response<Product>(product));
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            var isSuperAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isSuperAdmin").Value);
            if (isAdmin || isSuperAdmin)
            {
                _context.Entry(product).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            var isSuperAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isSuperAdmin").Value);
            if (isAdmin || isSuperAdmin)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            else
            {
                return Unauthorized();
            }
            return CreatedAtAction("GetProduct", new { id = product.id }, product);
        }

        // DELETE: api/Products/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isAdmin").Value);
            var isSuperAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "isSuperAdmin").Value);
            if (isAdmin || isSuperAdmin)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            else
            {
                return Unauthorized();
            }
            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.id == id);
        }
  
    }
}
