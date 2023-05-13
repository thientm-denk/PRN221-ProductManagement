using BussinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static object instanceLook = new object();

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> productList = null;
            try
            {
                MyStoreContext context = new MyStoreContext();
                productList = context.Products.ToList();
            }
            catch (Exception e)
            {

            }
            return productList;
        }

        public void UpdateAProduct(Product newProduct)
        {
            MyStoreContext context = null;
            try
            {
                context = new MyStoreContext();
                Product oldProduct = context.Products.Where(p => p.ProductId == newProduct.ProductId).ToList(0);
                if (oldProduct != null)
                {
                    context.Products.Update(newProduct);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
        }

    }
}
