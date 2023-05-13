using BussinessObjects.Models;
using Repositories;
using Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IProductRespository productRes = new ProductRepository();
        private ICategoryRespository categoryRes = new CagoryRepository();
        private List<Category> categories;
        private List<Product> products;

        public MainWindow()
        {
            InitializeComponent();
        }
       
        private void Awake(object sender, EventArgs e)
        {
            // resolve file path

            InitCategory();
            InitDataToShow();
        }

        private void InitCategory()
        {
            categories = new List<Category>();
            categories = categoryRes.GetCategories();

            CategoryBox.Items.Clear();
            foreach (var item in categories)
            {
                CategoryBox.Items.Add(item.CategoryName);
            }
        }

        private void InitDataToShow()
        {
            products = new List<Product>(); 
            products = productRes.GetProducts();
            foreach (var item in products)
            {
                //string[] row = { item.ProductId.ToString(), item.ProductName, item.Category.ToString(), item.UnitsInStock.ToString(), item.UnitPrice.ToString() };
                ////var listViewItem = new ListViewItem(row);
                ////listView1.Items.Add(listViewItem);

                //var itemx = new ListViewItem { Text = "Some Text for Column 1" };
                //itemx.Sub
                ListViewItem x = new ListViewItem();
                
                ProductView.Items.Add(x);
            }
           
        }
    }
}
