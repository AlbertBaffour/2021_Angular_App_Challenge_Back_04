using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dcode_shop_back.Filters
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public string brands { get; set; }
        public Decimal priceLow { get; set; }
        public Decimal priceHigh { get; set; }
        //== 1: producten die out f stock zijn  , 0:instock
        public int inStock { get; set; }

        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 8;
            this.SortDirection = "asc";
            this.brands = "";
            this.priceLow = 0;
            this.priceHigh = 100000;
            this.inStock = 1;
        }
        public PaginationFilter(int pageNumber, int pageSize, string sortBy = null, string sortDirection = "asc", string brands="" ,Decimal priceLow=0,Decimal priceHigh=100000, int inStock=1)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 8 ? 8 : pageSize;
            this.SortBy = sortBy;
            this.SortDirection = sortDirection;
            this.brands = brands;
            this.priceLow = priceLow < priceHigh ? priceLow:0 ;
            this.priceHigh =priceHigh>priceLow?priceHigh:100000;
            this.inStock = inStock;
        }
    }
}
