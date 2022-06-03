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
    public class CategoryService : ICategoryService
    {
        private readonly AppSettings _appSettings;
        private readonly ShopContext _context;
        private readonly IUriService _uriService;
        public CategoryService(IOptions<AppSettings> appSettings, ShopContext shopContext, IUriService uriService)
        {
            _appSettings = appSettings.Value;
            _context = shopContext;
            _uriService = uriService;
        }
        public async Task<List<int>> GetRecentCategoriesAsync(int id)
        {
            List<int> CategoryIds = new List<int>(); 
            var orders =await _context.Orders.Where(o => o.CustomerId == id).AsNoTracking().Select(o => o.Id).ToListAsync();
            foreach(int orderId in orders)
            {
                var orderProducts =await _context.OrderProducts.Where(o => o.OrderId == orderId).AsNoTracking().Select(p => p.ProductId).ToListAsync();
                foreach (int p in orderProducts)
                {
                    var catId = _context.Products.Where(c => c.id == p).Select(p => p.CategoryId).FirstOrDefault();
                    CategoryIds.Add(catId);
                }
            }
            CategoryIds = CategoryIds.Distinct().ToList();
            return CategoryIds;
        }
       public async Task<PagedResponse<List<Product>>> GetCategoryById(int id,PaginationFilter filter, PaginationFilter validFilter, string route)
       {
            //filters toepassen
            List<String> brds = (filter.brands != null && filter.brands.Trim() != "") ? filter.brands.ToLower().Split('%').ToList() : new List<string>();
            var totalRecords = 0;
            Category data;
            if (filter.brands!=null && filter.brands.Trim() != "")
            {
                //aantal producten van de category en de producten
                var cat =  _context.Categories.Include(c => c.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock<(validFilter.inStock==1?100000:1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).Where(p => brds.Contains(p.Brand.ToLower()))).AsNoTracking().SingleOrDefault(t => t.Id == id);
                totalRecords = cat.Products.Count;
                data = await _context.Categories.Include(c => c.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh).Where(p => brds.Contains(p.Brand.ToLower()))
                   .OrderByDescending(c => c.QuantityInStock)
                     .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                     .Take(validFilter.PageSize)).SingleOrDefaultAsync(t => t.Id == id);
            }
            else
            {
                //aantal producten van de category en de producten 
                var cat = await _context.Categories.Include(c => c.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh)).AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
                totalRecords = cat.Products.Count;
                data = await _context.Categories.Include(c => c.Products.Where(p => p.IsActive).Where(p => p.QuantityInStock < (validFilter.inStock == 1 ? 100000 : 1)).Where(p => p.QuantityInStock >= validFilter.inStock).Where(p => p.Price >= validFilter.priceLow).Where(p => p.Price <= validFilter.priceHigh)
               .OrderByDescending(c => c.QuantityInStock)
                 .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                 .Take(validFilter.PageSize)).SingleOrDefaultAsync(t => t.Id == id);
            }
                //sorteren 
            var sortValue = filter.SortBy != null ? filter.SortBy.ToLower() : null;
            List<Product> pagedData;
          
                if (filter.SortDirection != null && filter.SortDirection.ToLower() == "desc")
                {
                    pagedData = (sortValue == "name" || sortValue == "brand")
                    ? data.Products.OrderByDescending(FilterHelper.sortFuncStr(sortValue)).ToList()
                    : (sortValue == "price"
                    ? data.Products.OrderByDescending(FilterHelper.sortFuncDec(sortValue)).ToList()
                    : data.Products.ToList());
                }
                else
                {
                    pagedData = (sortValue == "name" || sortValue == "brand")
                   ? data.Products.OrderBy(FilterHelper.sortFuncStr(sortValue)).ToList()
                   : (sortValue == "price"
                   ? data.Products.OrderBy(FilterHelper.sortFuncDec(sortValue)).ToList()
                   : data.Products.ToList());
                }
            //category zonder product meegeven
            var currentCategory = await _context.Categories.Include(c => c.Products.Where(p => p.IsActive)).SingleOrDefaultAsync(t => t.Id == id);
            var pagedReponse = pagedData == null ? null:PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, _uriService, route, sortValue,filter.SortDirection);
            currentCategory.Products = null;
            var categoryWithoutInclude = currentCategory;
            pagedReponse.category = categoryWithoutInclude;
            return pagedReponse;
           }
     
    }
}
