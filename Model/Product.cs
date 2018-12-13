using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPADeltatre.Model
{
    public class Product
    {
        public int CatalogId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }

    public class CatalogPaginated
    {
        public List<Product> Products;
        public int totalPages;
        
    }
}
