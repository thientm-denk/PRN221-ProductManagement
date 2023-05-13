using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;
using Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private int selectedProductIndex = -1;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitCategory()
        {
            categories = new List<Category>();
            categories = categoryRes.GetCategories();
            CategoryBox.Items.Clear();

            if (categories == null)
            {
                MessageBox.Show("Connect to Db may failed, please check appsettings.json file", "ARLERT");
                throw new Exception();

            }
            foreach (var item in categories)
            {
                CategoryBox.Items.Add(item.CategoryName);
            }
        }

        private void InitDataProduct()
        {
            products = new List<Product>();
            products = productRes.GetProducts();
        }

        private void UpdateDisplayProduct()
        {
            var listItemShowed = new List<DisplayItem>();
            foreach (var item in products)
            {
                listItemShowed.Add(new DisplayItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    UnitsInStock = item.UnitsInStock,
                    CategoryId = categoryRes.GetCategoryById(item.CategoryId)?.CategoryName,
                });
            }

            ProductView.ItemsSource = listItemShowed;

            if (selectedProductIndex >= 0)
            {
                ProductView.SelectedIndex = selectedProductIndex;
            }
        }

        private string GetNameCategoryById(int? id)
        {
            foreach (var item in categories)
            {
                if (item.CategoryId == id) return item.CategoryName;
            }
            return "";
        }

        private int GetIdCategoryByName(string name)
        {
            foreach (var item in categories)
            {
                if (item.CategoryName == name) return item.CategoryId;
            }
            return -1;
        }
        private void ClearDisplayText()
        {
            ProductID.Text = "";
            ProductName.Text = "";
            CategoryBox.Text = "";
            UnitsInStock.Text = "";
            UnitPrice.Text = "";
        }
        #region EVENT
        private void Awake(object sender, EventArgs e)
        {
            // resolve file path

            InitCategory();
            InitDataProduct();
            UpdateDisplayProduct();
        }

        private void OnChangeSelectProduct(object sender, SelectionChangedEventArgs e)
        {

            var index = ProductView.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            selectedProductIndex = index;
            ProductID.Text = products[index].ProductId.ToString();
            ProductName.Text = products[index].ProductName;
            CategoryBox.Text = GetNameCategoryById(products[index].CategoryId);
            UnitsInStock.Text = products[index].UnitsInStock.ToString();
            UnitPrice.Text = products[index].UnitPrice.ToString();
        }
        private void OnChangeSelectedCaterogy(object sender, SelectionChangedEventArgs e)
        {

            //MessageBox.Show(GetIdCategoryByName(categories[CategoryBox.SelectedIndex].CategoryName).ToString());
        }
        private void OnClickUpdate(object sender, RoutedEventArgs e)
        {

            if (selectedProductIndex == -1)
            {
                MessageBox.Show("Please select item to update", "Alert");
                return;
            }
            try
            {
                var newProduct = products[selectedProductIndex];
                newProduct.ProductName = ProductName.Text;
                newProduct.CategoryId = GetIdCategoryByName(CategoryBox.Text) == -1 ? null : GetIdCategoryByName(CategoryBox.Text);
                newProduct.UnitsInStock = UnitsInStock.Text != string.Empty ? short.Parse(UnitsInStock.Text) : null;
                newProduct.UnitPrice = UnitPrice.Text != string.Empty ? decimal.Parse(UnitPrice.Text) : null;



                if (productRes.UpdateAProduct(newProduct))
                {
                     InitDataProduct();
                     UpdateDisplayProduct();
                    MessageBox.Show("Update successfully", "Alert");
                }
                else
                {
                    if (UnitsInStock.Text != string.Empty)
                    {
                        MessageBox.Show("UnitsInStock cannot be a string", "Alert");
                    }
                    else if (UnitPrice.Text != string.Empty)
                    {
                        MessageBox.Show("UnitPrice cannot be a string", "Alert");
                    }
                    else if (ProductName.Text == string.Empty)
                    {
                        MessageBox.Show("Product name cannot be emty", "Alert");
                    }
                    else
                    {
                        MessageBox.Show("Update Fail, Please try again", "Alert");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Update Fail, Please try again", "Alert");
            }
            

        }

        // Clean text for input
        private void OnClickCreate(object sender, RoutedEventArgs e)
        {
            var largestId = 0;
            foreach (var product in products)
            {
                if (product.ProductId >= largestId)
                {
                    largestId = product.ProductId;
                }
            }
            selectedProductIndex = -1;
            ProductView.SelectedIndex = -1;
            ClearDisplayText();
            ProductID.Text = "Creating new, id will auto generate" ;

        }

      
        private void OnClickSave(object sender, RoutedEventArgs e)
        {
            if (selectedProductIndex != -1)
            {
                MessageBox.Show("Click create first", "Alert");
            }
            else
            {
                try
                {
                    var newProduct = new Product();
                    newProduct.ProductName = ProductName.Text;
                    newProduct.CategoryId = GetIdCategoryByName(CategoryBox.Text) == -1 ? null : GetIdCategoryByName(CategoryBox.Text);
                    newProduct.UnitsInStock = UnitsInStock.Text != string.Empty ? short.Parse(UnitsInStock.Text) : null;
                    newProduct.UnitPrice =    UnitPrice.Text != string.Empty ? decimal.Parse(UnitPrice.Text) : null;
                    if (productRes.AddNewProduct(newProduct))
                    {
                        MessageBox.Show("Add successfully", "Alert");
                        InitDataProduct();
                        UpdateDisplayProduct();
                        selectedProductIndex = products.Count - 1;
                        ProductView.SelectedIndex = selectedProductIndex;
                    }
                    else
                    {
                        if(UnitsInStock.Text != string.Empty)
                        {
                            MessageBox.Show("UnitsInStock cannot be a string", "Alert");
                        }
                        else if (UnitPrice.Text != string.Empty)
                        {
                            MessageBox.Show("UnitPrice cannot be a string", "Alert");
                        }
                        else if(ProductName.Text == string.Empty)
                        {
                            MessageBox.Show("Product name cannot be emty", "Alert");
                        }
                        else
                        {
                            MessageBox.Show("Add Fail, Please try again", "Alert");
                        }
                       
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Add Fail, Please try again", "Alert");
                }
            }

        }
        private void OnClicDelete(object sender, RoutedEventArgs e)
        {
            if (selectedProductIndex == -1)
            {
                MessageBox.Show("Please choose item to delete", "Alert");
            }
            else
            {
                try
                {

                    if (productRes.DeleteAProduct(products[selectedProductIndex].ProductId))
                    {
                        MessageBox.Show("Delete successfully", "Alert");
                        InitDataProduct();
                        UpdateDisplayProduct();
                        selectedProductIndex = -1;
                        ProductView.SelectedIndex = selectedProductIndex;
                        ClearDisplayText();
                    }
                    else
                    {
                        MessageBox.Show("Delete fail", "Alert");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Delete fail", "Alert");
                }
            }

        }
        #endregion

        private class DisplayItem
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string? CategoryId { get; set; }
            public short? UnitsInStock { get; set; }
            public decimal? UnitPrice { get; set; }
        }


    }
}
