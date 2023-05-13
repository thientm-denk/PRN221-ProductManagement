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
        public bool AddNewProduct(Product newProduct)
        {
            return ProductDAO.Instance.AddNewProduct(newProduct);
        }

        public bool DeleteAProduct(int id)
        {
            return ProductDAO.Instance.DeleteAProduct(id);
        }

        public List<Product> GetProducts()
        {
            return ProductDAO.Instance.GetAllProducts();
        }

        public bool UpdateAProduct(Product newProduct)
        {
           return ProductDAO.Instance.UpdateAProduct(newProduct);
        }
    }
}
