using dcode_shop_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dcode_shop_back.Helpers
{
    public class FilterHelper
    {
        public static Func<Product, string> sortFuncStr(string sb)
        {
            Func<Product, string> orderByFunc = null;
            if (sb == "name")
                orderByFunc = item => item.Name;
            else if (sb == "brand")
                orderByFunc = item => item.Brand;
            return orderByFunc;
        }
        public static Func<Product, Decimal> sortFuncDec(string sb)
        {
            Func<Product, Decimal> orderByFunc = null;
            if (sb == "price")
                orderByFunc = item => item.Price;
            return orderByFunc;
        }
    }

}
