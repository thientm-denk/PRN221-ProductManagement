using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public bool UpdateAProduct(Product newProduct)
        {
            MyStoreContext context = null;
            try
            {
                context = new MyStoreContext();
                Product oldProduct = context.Products.Local.FirstOrDefault(p => p.ProductId == newProduct.ProductId)
                    ?? context.Products.FirstOrDefault(p => p.ProductId == newProduct.ProductId);

                if (oldProduct != null)
                {
                    context.Entry(oldProduct).State = EntityState.Detached;
                    context.Products.Update(newProduct);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool AddNewProduct(Product newProduct)
        {

            MyStoreContext context = null;
            try
            {
                context = new MyStoreContext();
                Product oldProduct = context.Products.Local.FirstOrDefault(p => p.ProductId == newProduct.ProductId)
                    ?? context.Products.FirstOrDefault(p => p.ProductId == newProduct.ProductId);

                if (oldProduct == null)
                {
                    context.Products.Add(newProduct);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool DeleteAProduct(int id)
        {
            try
            {
                var context = new MyStoreContext();
                Product oldProduct = context.Products.Where(p => p.ProductId == id).ToList()[0];

                if (oldProduct != null)
                {
                    context.Products.Remove(oldProduct);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
