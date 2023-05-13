using BussinessObjects.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementation
{
    public class CagoryRepository : ICategoryRespository
    {
        public List<Category> GetCategories()
        {
            return CategoryDAO.Instance.GetAllCategories();
        }
    }
}
