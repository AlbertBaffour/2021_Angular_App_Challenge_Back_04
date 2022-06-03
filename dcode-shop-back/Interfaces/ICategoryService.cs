using dcode_shop_back.Filters;
using dcode_shop_back.Models;
using dcode_shop_back.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dcode_shop_back.Services
{
    public interface ICategoryService
    {
        Task<PagedResponse<List<Product>>> GetCategoryById(int id,PaginationFilter filter, PaginationFilter validFilter, string route);
        Task<List<int>> GetRecentCategoriesAsync(int id);

    }
}
