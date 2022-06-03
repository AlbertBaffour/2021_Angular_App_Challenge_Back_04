using dcode_shop_back.Data;
using dcode_shop_back.Filters;
using dcode_shop_back.Helpers;
using dcode_shop_back.Models;
using dcode_shop_back.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dcode_shop_back.Services
{
    public class ProductService : IProductService
    {
        private readonly AppSettings _appSettings;
        private readonly ShopContext _context;
        private readonly IUriService _uriService;
        public ProductService(IOptions<AppSettings> appSettings, ShopContext shopContext , IUriService uriService)
        {
            _appSettings = appSettings.Value;
            _context = shopContext;
            _uriService = uriService;
        }
        public async Task<PagedResponse<List<Product>>> GetProducts(PaginationFilter filter, PaginationFilter validFilter, string route)
        {

            //sorteren +
            //filters toepassen

            List<String> brds =(filter.brands!=null && filter.brands.Trim()!="")? filter.brands.ToLower().Split('%').ToList():new List<string>();

            var sortValue = filter.SortBy != null ? filter.SortBy.ToLower() : null;
            List<Product> pagedData;
            var totalRecords = 0;
            if (filter.brands != null && filter.brands.Trim() != "")
            {
                if (filter.SortDirection != null && filter.SortDirection.ToLower() == "desc")
                {
                    pagedData = sortValue == "name"
                   ? await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).Where(p => brds.Contains(p.Brand.ToLower())).OrderByDescending(a => a.Name).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()
                   : (sortValue == "brand"
                   ? await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).Where(p => brds.Contains(p.Brand.ToLower())).OrderByDescending(p => p.Brand).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()
                   : (sortValue == "price"
                   ? await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).Where(p => brds.Contains(p.Brand.ToLower())).OrderByDescending(p => p.Price).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()
                   : await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).Where(p => brds.Contains(p.Brand.ToLower())).OrderByDescending(c => c.QuantityInStock).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()));
                }
                else
                {
                    pagedData = sortValue == "name"
                  ? await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).Where(p => brds.Contains(p.Brand.ToLower())).OrderByDescending(c => c.QuantityInStock).OrderBy(a => a.Name).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()
                  : (sortValue == "brand"
                  ? await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).Where(p => brds.Contains(p.Brand.ToLower())).OrderBy(p => p.Brand).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()
                  : (sortValue == "price"
                  ? await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).Where(p => brds.Contains(p.Brand.ToLower())).OrderBy(p => p.Price).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()
                  : await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).Where(p => brds.Contains(p.Brand.ToLower())).OrderByDescending(c => c.QuantityInStock).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()));
                }
                totalRecords = await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).Where(p => brds.Contains(p.Brand.ToLower())).CountAsync();
            }
            else {
                if (filter.SortDirection != null && filter.SortDirection.ToLower() == "desc")
                {
                    pagedData = sortValue == "name"
                   ? await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).OrderByDescending(a => a.Name).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()
                   : (sortValue == "brand"
                   ? await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).OrderByDescending(p => p.Brand).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()
                   : (sortValue == "price"
                   ? await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).OrderByDescending(p => p.Price).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()
                   : await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).OrderByDescending(c => c.QuantityInStock).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()));
                }
                else
                {
                    pagedData = sortValue == "name"
                  ? await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).OrderByDescending(c => c.QuantityInStock).OrderBy(a => a.Name).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()
                  : (sortValue == "brand"
                  ? await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).OrderBy(p => p.Brand).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()
                  : (sortValue == "price"
                  ? await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).OrderBy(p => p.Price).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()
                  : await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).OrderByDescending(c => c.QuantityInStock).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync()));
                }
                totalRecords = await _context.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).CountAsync();
            }
            //aantal records
            
            var pagedReponse =pagedData==null?null:PaginationHelper.CreatePagedReponse<Product>(pagedData, validFilter, totalRecords, _uriService, route, sortValue,filter.SortDirection);
            pagedReponse.brands = brds;pagedReponse.priceLow = validFilter.priceLow;pagedReponse.priceHigh = validFilter.priceHigh;
            return pagedReponse;
        }
        public async Task<List<Product>> GetProductsAdmin()
        {
       
             return await _context.Products.OrderByDescending(c => c.QuantityInStock).ToListAsync();
            
        }

        public async Task<List<Product>> GetProductsAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<string>> GetProductsBrands()
        {
       
             return await _context.Products.Select(p=>p.Brand).Where(b=>b!=null).Where(b => b.Trim() !="" ).ToListAsync();
            
        }
    }
}
