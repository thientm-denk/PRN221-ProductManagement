using BussinessObjects.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementation
{
    public class ProductRepository : IProductRespository
    {
        public List<Product> GetProducts()
        {
            return ProductDAO.Instance.GetAllProducts();
        }
    }
}
