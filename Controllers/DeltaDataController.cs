using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using SPADeltatre.Model;

namespace SPADeltatre.Controllers
{
    [Route("api/[controller]")]
    public class DeltaDataController : Controller
    {
        IConfiguration _config;
        public DeltaDataController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("[action]")]
        public CatalogPaginated GetProducts()
        {
            string connStr = _config.GetSection("Configuration").GetSection("ConnString").Value;

            List<Product> Prod = new List<Product>();
            int totalPages = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("sp_GetProducts", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = null;

                reader = cmd.ExecuteReader();

                reader.Read();

                totalPages = Convert.ToInt32(reader["totalPages"].ToString());

                reader.NextResult();
                while(reader.Read())
                {
                    Product p = new Product
                    {
                        CatalogId = Convert.ToInt32(reader["CatalogId"].ToString()),
                        ProductName = reader["ProductName"].ToString(),
                        Description = reader["Description"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"].ToString()),
                    };
                    Prod.Add(p);
                }

                return new CatalogPaginated { Products = Prod };

            }
        }

        
         
     
    }
}
