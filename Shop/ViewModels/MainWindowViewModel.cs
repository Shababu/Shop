using Microsoft.Win32;
using Shop.Infrastructure.Commands;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using Image = System.Drawing.Image;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Net;
using Newtonsoft.Json;

namespace Shop.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Свойства секции "Товары"

        private Visibility _TabControlForProductsVisibility = Visibility.Collapsed;

        public Visibility TabControlForProductsVisibility
        {
            get => _TabControlForProductsVisibility;
            set => Set(ref _TabControlForProductsVisibility, value);
        }

        #region Свойства для "Товары---Поиск"

        #region Товары->Поиск->Наименование товара
        private string _ProductFind_Name;
        public string ProductFind_Name
        {
            get => _ProductFind_Name;
            set => Set(ref _ProductFind_Name, value);
        }
        #endregion

        #region Товары->Поиск->Пол
        private string _ProductFind_Gender;
        public string ProductFind_Gender
        {
            get => _ProductFind_Gender;
            set => Set(ref _ProductFind_Gender, value);
        }
        #endregion

        #region Товары->Поиск->Бренд товара
        private string _ProductFind_Brand;
        public string ProductFind_Brand
        {
            get => _ProductFind_Brand;
            set => Set(ref _ProductFind_Brand, value);
        }
        #endregion

        #region Товары->Поиск->Тип товара
        private string _ProductFind_Type;
        public string ProductFind_Type
        {
            get => _ProductFind_Type;
            set => Set(ref _ProductFind_Type, value);
        }
        #endregion

        #region Товары->Поиск->Цвет товара
        private string _ProductFind_Color;
        public string ProductFind_Color
        {
            get => _ProductFind_Color;
            set => Set(ref _ProductFind_Color, value);
        }
        #endregion

        #region Товары->Поиск->Размер товара
        private string _ProductFind_Size;
        public string ProductFind_Size
        {
            get => _ProductFind_Size;
            set => Set(ref _ProductFind_Size, value);
        }
        #endregion

        #region Товары->Поиск->Цена товара
        private string _ProductFind_Price;
        public string ProductFind_Price
        {
            get => _ProductFind_Price;
            set => Set(ref _ProductFind_Price, value);
        }
        #endregion

        #region Товары->Поиск->Количество товара
        private string _ProductFind_Amount;
        public string ProductFind_Amount
        {
            get => _ProductFind_Amount;
            set => Set(ref _ProductFind_Amount, value);
        }
        #endregion

        #region Товары->Поиск->DataSourceItems

        private List<Product> _FindProductsResult;

        public List<Product> FindProductsResult
        {
            get => _FindProductsResult;
            set => Set(ref _FindProductsResult, value);
        }
        #endregion

        #region Товары->Поиск->DataSourceItemsVisibility

        private Visibility _DataGridForFindProductFormVisibility = Visibility.Collapsed;

        public Visibility DataGridForFindProductFormVisibility
        {
            get => _DataGridForFindProductFormVisibility;
            set => Set(ref _DataGridForFindProductFormVisibility, value);
        }
        #endregion

        #region Товары->Поиск->Products_ProductImageVisibility

        private Visibility _Products_ProductImageVisibility = Visibility.Collapsed;

        public Visibility Products_ProductImageVisibility
        {
            get => _Products_ProductImageVisibility;
            set => Set(ref _Products_ProductImageVisibility, value);
        }
        #endregion

        #region Товары->Поиск->Products_Search_SelectedProductPhoto

        private BitmapImage _Products_Search_SelectedProductPhoto;

        public BitmapImage Products_Search_SelectedProductPhoto
        {
            get => _Products_Search_SelectedProductPhoto;
            set => Set(ref _Products_Search_SelectedProductPhoto, value);
        }
        #endregion        

        #region Товары->Поиск->Products_ImageBorderColor

        private System.Windows.Media.Brush _Products_ImageBorderColor = System.Windows.Media.Brushes.White;

        public System.Windows.Media.Brush Products_ImageBorderColor
        {
            get => _Products_ImageBorderColor;
            set => Set(ref _Products_ImageBorderColor, value);
        }
        #endregion

        #region Товары->Поиск->Products_Search_DataGridSelectedItem

        private Product _Products_Search_DataGridSelectedItem;
        public Product Products_Search_DataGridSelectedItem
        {
            get => _Products_Search_DataGridSelectedItem;
            set
            {
                Set(ref _Products_Search_DataGridSelectedItem, value);
                ShopAccessLibrary library = new ShopAccessLibrary();
                
                using (MemoryStream memoryStream = new MemoryStream(library.GetProductPhoto(_Products_Search_DataGridSelectedItem)))
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = memoryStream;
                    bi.DecodePixelWidth = 200;
                    bi.DecodePixelHeight = 250;
                    bi.EndInit();
                    Products_Search_SelectedProductPhoto = bi;
                }
                Products_ProductImageVisibility = Visibility.Visible;
                Products_ImageBorderColor = new SolidColorBrush(Colors.LightGreen);
            }
        }

        #endregion        

        #region Товары->Поиск->Кнопка Изменить Visibility

        private Visibility _UpdateButtonProductFormVisibility = Visibility.Collapsed;

        public Visibility UpdateButtonProductFormVisibility
        {
            get => _UpdateButtonProductFormVisibility;
            set => Set(ref _UpdateButtonProductFormVisibility, value);
        }
        #endregion

        #region Товары->Поиск->Кнопка Удалить Visibility

        private Visibility _DeleteButtonProductFormVisibility = Visibility.Collapsed;

        public Visibility DeleteButtonProductFormVisibility
        {
            get => _DeleteButtonProductFormVisibility;
            set => Set(ref _DeleteButtonProductFormVisibility, value);
        }
        #endregion

        #endregion

        #region Свойства для "Товары---Поиск по Id"
        private string _ProductFindByID_Id = null;
        public string ProductFindById_Id
        {
            get => _ProductFindByID_Id;
            set => Set(ref _ProductFindByID_Id, value);
        }
        private List<Product> _FindProductsByIdResult;
        public List<Product> FindProductsByIdResult
        {
            get => _FindProductsByIdResult;
            set => Set(ref _FindProductsByIdResult, value);
        }

        private Visibility _DataGridForFindProductByIdFormVisibility = Visibility.Collapsed;
        public Visibility DataGridForFindProductByIdFormVisibility
        {
            get => _DataGridForFindProductByIdFormVisibility;
            set => Set(ref _DataGridForFindProductByIdFormVisibility, value);
        }
        #endregion

        #region Свойства для "Товары---Добавить"

        #region Товары->Добавить->Наименование товара
        private string _ProductAdd_Name;
        public string ProductAdd_Name
        {
            get => _ProductAdd_Name;
            set => Set(ref _ProductAdd_Name, value);
        }
        #endregion

        #region Товары->Добавить->Бренд товара
        private string _ProductAdd_Brand;
        public string ProductAdd_Brand
        {
            get => _ProductAdd_Brand;
            set => Set(ref _ProductAdd_Brand, value);
        }
        #endregion

        #region Товары->Добавить->Тип товара
        private string _ProductAdd_Type;
        public string ProductAdd_Type
        {
            get => _ProductAdd_Type;
            set => Set(ref _ProductAdd_Type, value);
        }
        #endregion

        #region Товары->Добавить->Цвет товара
        private string _ProductAdd_Color;
        public string ProductAdd_Color
        {
            get => _ProductAdd_Color;
            set => Set(ref _ProductAdd_Color, value);
        }
        #endregion

        #region Товары->Добавить->Размер товара

        private string _ProductAdd_Size;
        public string ProductAdd_Size
        {
            get => _ProductAdd_Size;
            set => Set(ref _ProductAdd_Size, value);
        }
        #endregion

        #region Товары->Добавить->Цена товара
        private string _ProductAdd_Price;
        public string ProductAdd_Price
        {
            get => _ProductAdd_Price;
            set => Set(ref _ProductAdd_Price, value);
        }
        #endregion

        #region Товары->Добавить->Количество товара
        private string _ProductAdd_Amount;
        public string ProductAdd_Amount
        {
            get => _ProductAdd_Amount;
            set => Set(ref _ProductAdd_Amount, value);
        }
        #endregion

        #endregion

        #region Свойства для "Товары---Все товары"

        #region Свойство "Товары---Все товары---Items Source"

        private List<Product> _AllProducts = new List<Product>();

        public List<Product> AllProducts
        {
            get => _AllProducts;
            set => Set(ref _AllProducts, value);
        }

        #endregion

        #region Свойство "Товары---Все товары---DataGrid Visibility"
        private Visibility _DataGridForAllProductsVisibility = Visibility.Collapsed;

        public Visibility DataGridForAllProductsVisibility
        {
            get => _DataGridForAllProductsVisibility;
            set => Set(ref _DataGridForAllProductsVisibility, value);
        }
        #endregion
        #endregion

        #endregion

        #region Команды секции "Товары"

        #region Команда "Открыть меню Товары"
        public ICommand OpenProductsMenuWindowCommand { get; }
        public void OnOpenProductsMenuWindowCommandExecute(object p)
        {
            TabControlForOrdersVisibility = Visibility.Collapsed;
            TabControlForProductsVisibility = Visibility.Visible;
            FindProductsResult = null;
            HideAllDataGrids();
            ClearAllTextBoxes();
            Products_ProductImageVisibility = Visibility.Collapsed;
            Products_ImageBorderColor = new SolidColorBrush(Colors.White);
        }

        public bool CanOpenProductsMenuWindowCommandExecute(object p)
        {
                return true;
        }
        #endregion

        #region Команда "Товары---Поиск---Поиск"
        public ICommand FindProductCommand { get; }
        public void OnFindProductCommandExecute(object p)
        {
            if (ProductFind_Name != null ||
                ProductFind_Gender != null ||
                ProductFind_Brand != null ||
                ProductFind_Type != null ||
                ProductFind_Color != null ||
                ProductFind_Size != null ||
                ProductFind_Price != null ||
                ProductFind_Amount != null)
            {
                ShopAccessLibrary library = new ShopAccessLibrary();
                FindProductsResult = library.SearchProductByCharacteristics(ProductFind_Name, ProductFind_Gender, ProductFind_Brand,
                    ProductFind_Type, ProductFind_Color, ProductFind_Size, ProductFind_Price, ProductFind_Amount);
                if (FindProductsResult.Count > 0)
                {
                    DataGridForFindProductFormVisibility = UpdateButtonProductFormVisibility = DeleteButtonProductFormVisibility = Visibility.Visible;
                }
                else
                {
                    DataGridForFindProductFormVisibility = UpdateButtonProductFormVisibility = DeleteButtonProductFormVisibility = Visibility.Collapsed;
                    MessageBox.Show("Ничего не найдено", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            Products_Search_SelectedProductPhoto = null;
            Products_ImageBorderColor = new SolidColorBrush(Colors.White);
        }

        public bool CanFindProductCommandExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда "Товары---Поиск---Сброс"

        public ICommand ClearFindProductFormCommand { get; }
        public void OnClearFindProductFormCommandExecute(object p)
        {
            ClearAllTextBoxes();
            FindProductsResult = null;
            DataGridForFindProductFormVisibility = UpdateButtonProductFormVisibility = DeleteButtonProductFormVisibility = Products_ProductImageVisibility = Visibility.Collapsed;
            Products_Search_SelectedProductPhoto = null;
            Products_ImageBorderColor = new SolidColorBrush(Colors.White);
        }

        public bool CanClearFindProductFormCommandExecute(object p)
        {
            return true;
        }

        #endregion

        #region Команда "Товары---Поиск---Удалить"

        public bool CanDeleteProductCommandExecute(object p)
        {
            return true;
        }

        #endregion

        #region Команда "Товары---Поиск по ID---Поиск"

        public ICommand FindProductByIdCommand { get; }

        public void OnFindProductByIdCommandExecute(object p)
        {
            ShopAccessLibrary library = new ShopAccessLibrary();
            int id = Convert.ToInt32(ProductFindById_Id);
            List<Product> product = new List<Product>();

            try
            {
                product.Add(library.GetProductById(id));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            FindProductsByIdResult = product;
            DataGridForFindProductByIdFormVisibility = Visibility.Visible;
            
        }

        public bool CanFindProductByIdCommandExecute(object p)
        {
            return true;
        }

        #endregion

        #region Команда "Товары---Добавить---Поиск изображения"
        public ICommand ProductPhotoSearchCommand { get; }

        private BitmapImage _ImageToAdd;
        public BitmapImage ImageToAdd { get => _ImageToAdd; set => Set(ref _ImageToAdd, value); }

        private byte[] _ProductImageInBytes;
        public byte[] ProductImageInBytes { get => _ProductImageInBytes; set => Set(ref _ProductImageInBytes, value); }

        private string _ProductImageName;
        public string ProductImageName { get => _ProductImageName; set => Set(ref _ProductImageName, value); }

        public void OnProductPhotoSearchCommandExecute(object p)
        {
            string filePath = string.Empty;
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                filePath = fileDialog.FileName;
                ProductImageName = fileDialog.SafeFileName;

                System.Drawing.Image img = System.Drawing.Image.FromFile(filePath);
                ImageConverter converter = new ImageConverter();
                ProductImageInBytes = (byte[])converter.ConvertTo(img, typeof(byte[]));

                Uri ImageToAddUri = new Uri(filePath);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = ImageToAddUri;
                bi.DecodePixelWidth = 300;
                bi.DecodePixelHeight = 370;
                bi.EndInit();

                ImageToAdd = bi;    
                
            }
        }

        public bool CanProductPhotoSearchCommandExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда "Товары---Добавить---Добавить"

        public ICommand AddProductCommand { get; }
        public void OnAddProductCommandExecute(object p)
        {
            if (ProductAdd_Name != null &&
                ProductFind_Gender != null &&
                ProductAdd_Brand != null &&
                ProductAdd_Type != null &&
                ProductAdd_Color != null &&
                ProductAdd_Size != null &&
                ProductAdd_Price != null &&
                ProductAdd_Amount != null)
            {
                ShopAccessLibrary library = new ShopAccessLibrary();
                try
                {
                    library.AddProduct(ProductAdd_Name, ProductFind_Gender, ProductAdd_Brand,
                        ProductAdd_Type, ProductAdd_Color, ProductAdd_Size, ProductAdd_Price, ProductAdd_Amount, ProductImageInBytes, ProductImageName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ClearAllTextBoxes();
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public bool CanAddProductCommandExecute(object p)
        {
            return true;
        }

        #endregion

        #region Команда "Товары---Все товары"

        public ICommand ShowAllProductsCommand { get; }

        public void OnShowAllProductsCommandExecute(object p)
        {
            ShopAccessLibrary library = new ShopAccessLibrary();
            AllProducts = library.GetAllProducts();
            DataGridForAllProductsVisibility = Visibility.Visible;
        }

        public bool CanShowAllProductsCommandExecute(object p)
        {
            return true;
        }

        #endregion

        #endregion

        #region Свойства секции "Заказы"

        #region Свойство видимости для TabControl
        private Visibility _TabControlForOrdersVisibility = Visibility.Collapsed;

        public Visibility TabControlForOrdersVisibility
        {
            get => _TabControlForOrdersVisibility;
            set => Set(ref _TabControlForOrdersVisibility, value);
        }
        #endregion

        #region Свойство ItemsSource для "Активные заказы"
        private List<ActiveOrder> _ActiveOrdersItemsSource;

        public List<ActiveOrder> ActiveOrdersItemsSource
        {
            get => _ActiveOrdersItemsSource;
            set => Set(ref _ActiveOrdersItemsSource, value);
        }
        #endregion

        #region Заказы->Поиск заказа->OrderId

        private string _ActiveOrderId;
        public string ActiveOrderId
        {
            get => _ActiveOrderId;
            set => Set(ref _ActiveOrderId, value);
        }

        #endregion

        #region Свойство видимости DataGrid для "Поиск заказа"

        private Visibility _DataGridForFindOrderByIdFormVisibility = Visibility.Collapsed;

        public Visibility DataGridForFindOrderByIdFormVisibility
        {
            get => _DataGridForFindOrderByIdFormVisibility;
            set => Set(ref _DataGridForFindOrderByIdFormVisibility, value);
        }

        #endregion

        #region Свойство ItemsSource для "Поиск заказа"
        private List<ActiveOrder> _FoundOrderItemsSource;

        public List<ActiveOrder> FoundOrderItemsSource
        {
            get => _FoundOrderItemsSource;
            set => Set(ref _FoundOrderItemsSource, value);
        }
        #endregion

        #region Свойство SelectedItem для выбранного товара
        private Product _SelectedProductForCart;
        public Product SelectedProductForCart 
        { 
            get => _SelectedProductForCart; 
            set => Set(ref _SelectedProductForCart, value); 
        }
        #endregion

        #region Свойство ItemsSource для "Корзина"
        private List<Product> _CartItemsSource;

        public List<Product> CartItemsSource
        {
            get => _CartItemsSource;
            set => Set(ref _CartItemsSource, value);
        }
        #endregion

        #region Свойство видимости DataGrid для "Корзина"

        private Visibility _DataGridForCartVisibility = Visibility.Collapsed;

        public Visibility DataGridForCartVisibility
        {
            get => _DataGridForCartVisibility;
            set => Set(ref _DataGridForCartVisibility, value);
        }

        #endregion

        #endregion

        #region Команды секции "Заказы"

        #region Команда "Открыть меню Заказы"
        public ICommand OpenOrdersMenuWindowCommand { get; }
        public void OnOpenOrdersMenuWindowCommandExecute(object p)
        {
            ShopAccessLibrary library = new ShopAccessLibrary();
            ActiveOrdersItemsSource = library.GetActiveOrders();
            TabControlForProductsVisibility = Visibility.Collapsed;
            TabControlForOrdersVisibility = Visibility.Visible;
            FindProductsResult = null;
            Products_ProductImageVisibility = Visibility.Collapsed;
            Products_ImageBorderColor = new SolidColorBrush(Colors.White);
            Products_Search_SelectedProductPhoto = null;
            UpdateButtonProductFormVisibility = DeleteButtonProductFormVisibility = Visibility.Collapsed;
            HideAllDataGrids();
            ClearAllTextBoxes();
        }

        public bool CanOpenOrdersMenuWindowCommandExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда "Заказы---Поиск---Поиск"
        public ICommand FindOrderByIdCommand { get; }

        public void OnFindOrderByIdCommandExecute(object p)
        {
            int orderId;
            ShopAccessLibrary library = new ShopAccessLibrary();
            if (int.TryParse(ActiveOrderId, out orderId))
            {
                FoundOrderItemsSource = library.GetOrderById(orderId);
                if(FoundOrderItemsSource.Count == 0)
                {
                    DataGridForFindOrderByIdFormVisibility = Visibility.Collapsed;
                    MessageBox.Show("Заказа с данным идентификатором нет в Базе данных", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                DataGridForFindOrderByIdFormVisibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Некорректный ввод. Введите число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool CanFindOrderByIdCommandExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда "Заказы---Поиск---Добавить в корзину"
        public ICommand AddProductToCartCommand { get; }

        public void OnAddProductToCartCommandExecute(object p)
        {
            if(CartItemsSource == null)
            {
                CartItemsSource = new List<Product>();
            }
            SelectedProductForCart.ProductAmount = 1;
            CartItemsSource.Add(SelectedProductForCart);
            if (DataGridForCartVisibility != Visibility.Visible)
            {
                DataGridForCartVisibility = Visibility.Visible;
            }
        }

        public bool CanAddProductToCartCommandExecute(object p)
        {
            return true;
        }
        #endregion

        #endregion
        public MainWindowViewModel()
        {
            OpenProductsMenuWindowCommand = new LambdaCommand(OnOpenProductsMenuWindowCommandExecute, CanOpenProductsMenuWindowCommandExecute);
            FindProductCommand = new LambdaCommand(OnFindProductCommandExecute, CanFindProductCommandExecute);
            ClearFindProductFormCommand = new LambdaCommand(OnClearFindProductFormCommandExecute, CanClearFindProductFormCommandExecute);
            FindProductByIdCommand = new LambdaCommand(OnFindProductByIdCommandExecute, CanFindProductByIdCommandExecute);
            AddProductCommand = new LambdaCommand(OnAddProductCommandExecute, CanAddProductCommandExecute);
            ShowAllProductsCommand = new LambdaCommand(OnShowAllProductsCommandExecute, CanShowAllProductsCommandExecute);
            OpenOrdersMenuWindowCommand = new LambdaCommand(OnOpenOrdersMenuWindowCommandExecute, CanOpenOrdersMenuWindowCommandExecute);
            FindOrderByIdCommand = new LambdaCommand(OnFindOrderByIdCommandExecute, CanFindOrderByIdCommandExecute);
            AddProductToCartCommand = new LambdaCommand(OnAddProductToCartCommandExecute, CanAddProductToCartCommandExecute);
            ProductPhotoSearchCommand = new LambdaCommand(OnProductPhotoSearchCommandExecute, CanProductPhotoSearchCommandExecute);

            LabelXrpPrice = string.Format($"{XrpPrice.Symbol} : {XrpPrice.Price:F4}");
        }
   
        public void HideAllDataGrids()
        {
            DataGridForAllProductsVisibility = DataGridForFindProductFormVisibility = 
                DataGridForFindProductByIdFormVisibility = DataGridForFindOrderByIdFormVisibility = Products_ProductImageVisibility = Visibility.Collapsed;
            Products_ImageBorderColor = new SolidColorBrush(Colors.White);
        }

        public void ClearAllTextBoxes()
        {
            ProductAdd_Name = ProductFind_Gender = ProductAdd_Brand = ProductFind_Brand = ProductAdd_Type = 
                ProductFind_Type = ProductAdd_Color = ProductFind_Color = ProductAdd_Size = ProductFind_Size 
                = ProductAdd_Price = ProductFind_Price = ProductAdd_Amount = ProductFind_Amount = null;
        }




        #region Удалить XRP
        private string _LabelXrpPrice = string.Empty;

        public string LabelXrpPrice
        {
            get => _LabelXrpPrice;
            set => Set(ref _LabelXrpPrice, value);
        }

        private Xrp _XrpPrice = new Xrp();

        public Xrp XrpPrice 
        {
            get => GetXrpPrice();
            set => Set(ref _XrpPrice, value); 
        }

        public Xrp GetXrpPrice()
        {
            string url = "https://api.binance.com/api/v3/ticker/price?symbol=XRPUSDT";

            HttpWebRequest HTTPrequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse HTTPresponse = (HttpWebResponse)HTTPrequest.GetResponse();

            string response;

            using (StreamReader reader = new StreamReader(HTTPresponse.GetResponseStream()))
            {
                response = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<Xrp>(response);
        }

        #endregion
    }
}
