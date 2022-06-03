using dcode_shop_back.Filters;
using dcode_shop_back.Models;
using dcode_shop_back.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dcode_shop_back.Services
{
    public interface IProductService
    {
        Task<PagedResponse<List<Product>>> GetProducts(PaginationFilter filter, PaginationFilter validFilter, string route);

        Task<List<Product>> GetProductsAll();

        Task<List<Product>> GetProductsAdmin();

        Task<List<String>> GetProductsBrands();
    }
}
