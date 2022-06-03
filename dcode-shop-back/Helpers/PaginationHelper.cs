using dcode_shop_back.Filters;
using dcode_shop_back.Services;
using dcode_shop_back.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dcode_shop_back.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilter validFilter, int totalRecords, IUriService uriService, string route,string sortBy=null , string sortDirection= "asc")
        {
            var respose = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize,validFilter.SortBy,validFilter.SortDirection,validFilter.brands,validFilter.priceLow,validFilter.priceHigh), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize, validFilter.SortBy, validFilter.SortDirection, validFilter.brands, validFilter.priceLow, validFilter.priceHigh), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize, validFilter.SortBy, validFilter.SortDirection, validFilter.brands, validFilter.priceLow, validFilter.priceHigh), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize, validFilter.SortBy, validFilter.SortDirection, validFilter.brands, validFilter.priceLow, validFilter.priceHigh), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            respose.SortBy = sortBy;
            respose.SortDirection = sortDirection;
            respose.priceLow = validFilter.priceLow;
            respose.priceHigh = validFilter.priceHigh;
            respose.inStock = validFilter.inStock;

            return respose;
        }
    }
}
