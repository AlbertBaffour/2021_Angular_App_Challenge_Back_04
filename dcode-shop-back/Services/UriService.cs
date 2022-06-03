using dcode_shop_back.Filters;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dcode_shop_back.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            if (filter.brands != null && filter.brands.Trim() != "") { modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "brands", filter.brands); }
            if (filter.priceLow != 0){ modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "priceLow", filter.priceLow.ToString());}
            if (filter.priceHigh != 0 && filter.priceHigh != 100000){ modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "priceHigh", filter.priceHigh.ToString());}
             modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "inStock", filter.inStock.ToString());
            return new Uri(modifiedUri);
        }
    }
}
