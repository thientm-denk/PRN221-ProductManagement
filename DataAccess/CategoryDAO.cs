using BussinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static object instanceLook = new object();
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Category> GetAllCategories()
        {
            List<Category> list = null;
            try
            {
                MyStoreContext context = new MyStoreContext();
                list = context.Categories.ToList();
            }
            catch (Exception e)
            {

            }
            return list;
        }

        public Category GetCategoryById(int? id)
        {
            Category category = null;
            try
            {
                MyStoreContext context = new MyStoreContext();
                category = context.Categories.Where(c => c.CategoryId == id).ToList()[0]; 
            }
            catch (Exception e)
            {

            }
            return category;
        }

        public Category GetCategoryByName(string? name)
        {
            Category category = null;
            try
            {
                MyStoreContext context = new MyStoreContext();
                category = context.Categories.Where(c => c.CategoryName == name).ToList()[0];
            }
            catch (Exception e)
            {
                
            }
            return category;
        }
    }
}
