using BussinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IProductRespository


    {
        public List<Product> GetProducts();
        public bool UpdateAProduct(Product newProduct);
        public bool AddNewProduct(Product newProduct);
        public bool DeleteAProduct(int id);
    }
}
