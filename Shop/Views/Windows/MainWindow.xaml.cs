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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void ToolbarButton_Click(object sender, RoutedEventArgs e)
        //{
        //    displayGrid.ItemsSource = null;
        //    HideAndClearAllElements();
        //    ShopAccessLibrary library = new ShopAccessLibrary();

        //    switch (((Button)sender).Name)
        //    {
        //        case "btnProducts":
        //            ProductsMenuWindow productsMenuWindow = new ProductsMenuWindow();
        //            productsMenuWindow.Owner = this;
        //            productsMenuWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //            productsMenuWindow.Show();                    
        //            break;
        //        case "btnOrders":
        //            displayGrid.ItemsSource = library.GetOrdersToComplete();
        //            displayGrid.Visibility = Visibility.Visible;
        //            break;
        //        case "btnEmpolyees":
        //            panelEmployees.Visibility = Visibility.Visible;
        //            break;
        //    }
        //}

        //private void HideAndClearAllElements()
        //{
        //    displayGrid.Visibility = Visibility.Collapsed;
        //    panelEmployees.Visibility = Visibility.Collapsed;
        //    gridAddEmployeeForm.Visibility = Visibility.Collapsed;
        //    gridAddProductForm.Visibility = Visibility.Collapsed;
        //    ((App)Application.Current).MenuPick = App.MenuPicks.NullPick;
        //    ClearProductForm();
        //}

        //private void btnAddEmployees_Click(object sender, MouseButtonEventArgs e)
        //{
        //    HideAndClearAllElements();
        //    gridAddEmployeeForm.Visibility = Visibility.Visible;
        //}

        //private void btnBackToEmployeesMenu_Click(object sender, RoutedEventArgs e)
        //{
        //    HideAndClearAllElements();
        //    panelEmployees.Visibility = Visibility.Visible;
        //}

        //private void mainWindow_Activated(object sender, EventArgs e)
        //{
        //    if (((App)Application.Current).MenuPick == App.MenuPicks.AllProducts)
        //    {
        //        ShopAccessLibrary library = new ShopAccessLibrary();
        //        displayGrid.ItemsSource = library.GetAllProducts();
        //        displayGrid.Visibility = Visibility.Visible;
        //    }
        //    else if (((App)Application.Current).MenuPick == App.MenuPicks.AddProduct)
        //    {
        //        gridAddProductForm.Visibility = Visibility.Visible;
        //        btnSearchProduct.Visibility = Visibility.Collapsed;
        //        btnAddProduct.Visibility = Visibility.Visible;
        //    }
        //    else if (((App)Application.Current).MenuPick == App.MenuPicks.ProductsSearch)
        //    {
        //        gridAddProductForm.Visibility = Visibility.Visible;
        //        btnAddProduct.Visibility = Visibility.Collapsed;
        //        btnSearchProduct.Visibility = Visibility.Visible;
        //    }
        //}

        //private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        //{
        //    if (IsAddProductsFormNotNull() == true)
        //    {
        //        ShopAccessLibrary library = new ShopAccessLibrary();
        //        library.AddProduct(
        //            txtProductName_Add.Text,
        //            comboProductBrand_Add.Text,
        //            comboProductType_Add.Text,
        //            comboProductColor_Add.Text,
        //            comboProductSize_Add.Text,
        //            txtProductPrice_Add.Text,
        //            txtProductAmount_Add.Text);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //    }
        //}

        //private bool IsAddProductsFormNotNull()
        //{
        //    if (txtProductName_Add.Text != string.Empty && txtProductPrice_Add.Text != string.Empty && 
        //        txtProductAmount_Add.Text != string.Empty && comboProductBrand_Add.Text != string.Empty && 
        //        comboProductType_Add.Text != string.Empty && comboProductColor_Add.Text != string.Empty && 
        //        comboProductSize_Add.Text != string.Empty)
        //        return true;
        //    return false;
        //}

        //private void btnSearchProduct_Click(object sender, RoutedEventArgs e)
        //{
        //    ShopAccessLibrary library = new ShopAccessLibrary();
        //    displayGrid.ItemsSource = library.SearchProductByCharacteristics(txtProductName_Add.Text, comboProductBrand_Add.Text,
        //        comboProductType_Add.Text, comboProductColor_Add.Text, comboProductSize_Add.Text,
        //        txtProductPrice_Add.Text, txtProductAmount_Add.Text);
        //    displayGrid.Visibility = Visibility.Visible;
        //}

        //private void btnClearProductForm_Click(object sender, RoutedEventArgs e)
        //{
        //    ClearProductForm();
        //}

        //private void ClearProductForm()
        //{
        //    txtProductAmount_Add.Text = txtProductName_Add.Text = txtProductPrice_Add.Text = string.Empty;
        //    comboProductBrand_Add.Text = comboProductColor_Add.Text = comboProductSize_Add.Text = comboProductType_Add.Text = string.Empty;
        //}
    }
}
