using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Shop;
namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для ProductsSearchMenu.xaml
    /// </summary>
    public partial class ProductsSearchMenu : Window
    {
        public ProductsSearchMenu()
        {
            InitializeComponent();
        }

        private void btnMenuAllProducts_Click(object sender, RoutedEventArgs e)
        {            
            ((App)Application.Current).MenuPick = App.MenuPicks.AllProducts;
            this.Close();
        }

        private void btnMenuAddProduct_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).MenuPick = App.MenuPicks.AddProduct;
            this.Close();
        }

        private void btnMenuSearchProduct_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).MenuPick = App.MenuPicks.ProductsSearch;
            this.Close();
        }
    }
}
