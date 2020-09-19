using Shop.Infrastructure.Commands;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;

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

        #region Товары->Форма->Наименование товара
        private string _ProductFind_Name;
        public string ProductFind_Name
        {
            get => _ProductFind_Name;
            set => Set(ref _ProductFind_Name, value);
        }
        #endregion

        #region Товары->Форма->Бренд товара
        private string _ProductFind_Brand;
        public string ProductFind_Brand
        {
            get => _ProductFind_Brand;
            set => Set(ref _ProductFind_Brand, value);
        }
        #endregion

        #region Товары->Форма->Тип товара
        private string _ProductFind_Type;
        public string ProductFind_Type
        {
            get => _ProductFind_Type;
            set => Set(ref _ProductFind_Type, value);
        }
        #endregion

        #region Товары->Форма->Цвет товара
        private string _ProductFind_Color;
        public string ProductFind_Color
        {
            get => _ProductFind_Color;
            set => Set(ref _ProductFind_Color, value);
        }
        #endregion

        #region Товары->Форма->Размер товара
        private string _ProductFind_Size;
        public string ProductFind_Size
        {
            get => _ProductFind_Size;
            set => Set(ref _ProductFind_Size, value);
        }
        #endregion

        #region Товары->Форма->Цена товара
        private string _ProductFind_Price;
        public string ProductFind_Price
        {
            get => _ProductFind_Price;
            set => Set(ref _ProductFind_Price, value);
        }
        #endregion

        #region Товары->Форма->Количество товара
        private string _ProductFind_Amount;
        public string ProductFind_Amount
        {
            get => _ProductFind_Amount;
            set => Set(ref _ProductFind_Amount, value);
        }
        #endregion

        #region Свойства для "Товары---Поиск"

        private List<Product> _FindProductsResult;

        public List<Product> FindProductsResult
        {
            get => _FindProductsResult;
            set => Set(ref _FindProductsResult, value);
        }

        private Visibility _DataGridForFindProductFormVisibility = Visibility.Collapsed;

        public Visibility DataGridForFindProductFormVisibility
        {
            get => _DataGridForFindProductFormVisibility;
            set => Set(ref _DataGridForFindProductFormVisibility, value);
        }
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

        #endregion

        #region Команды

        #region Команда "Открыть меню Товары"
        public ICommand OpenProductsMenuWindow { get; }
        private bool IsProductsMenuOpen { get; set; } = false;
        public void OnOpenProductsMenuWindowExecute(object p)
        {
            TabControlForProductsVisibility = Visibility.Visible;
            IsProductsMenuOpen = true;
        }

        public bool CanOpenProductsMenuWindowExecute(object p)
        {
            if (!IsProductsMenuOpen)
                return true;
            else
                return false;
        }
        #endregion

        #region Команда "Товары---Поиск---Поиск"
        public ICommand FindProductCommand { get; }
        public void OnFindProductCommandExecute(object p)
        {
            if (ProductFind_Name != null ||
                ProductFind_Brand != null ||
                ProductFind_Type != null ||
                ProductFind_Color != null ||
                ProductFind_Size != null ||
                ProductFind_Price != null ||
                ProductFind_Amount != null)
            {
                ShopAccessLibrary library = new ShopAccessLibrary();
                FindProductsResult = library.SearchProductByCharacteristics(ProductFind_Name, ProductFind_Brand,
                    ProductFind_Type, ProductFind_Color, ProductFind_Size, ProductFind_Price, ProductFind_Amount);
                DataGridForFindProductFormVisibility = Visibility.Visible;
            }
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
            ProductFind_Name = ProductFind_Brand = ProductFind_Type = ProductFind_Color = 
                ProductFind_Size = ProductFind_Price = ProductFind_Amount = null;
            FindProductsResult = null;
            DataGridForFindProductFormVisibility = Visibility.Collapsed;
        }

        public bool CanClearFindProductFormCommandExecute(object p)
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
            product.Add(library.GetProductById(id));
            FindProductsByIdResult = product;
            DataGridForFindProductByIdFormVisibility = Visibility.Visible;
        }

        public bool CanFindProductByIdCommandExecute(object p)
        {
            return true;
        }

        #endregion

        #endregion
        public MainWindowViewModel()
        {
            OpenProductsMenuWindow = new LambdaCommand(OnOpenProductsMenuWindowExecute, CanOpenProductsMenuWindowExecute);
            FindProductCommand = new LambdaCommand(OnFindProductCommandExecute, CanFindProductCommandExecute);
            ClearFindProductFormCommand = new LambdaCommand(OnClearFindProductFormCommandExecute, CanClearFindProductFormCommandExecute);
            FindProductByIdCommand = new LambdaCommand(OnFindProductByIdCommandExecute, CanFindProductByIdCommandExecute);
        }
    }
}
