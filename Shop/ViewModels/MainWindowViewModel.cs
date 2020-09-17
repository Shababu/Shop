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
        #region Товары->Форма->Наименование товара
        private string _ProductName;
        public string ProductName
        {
            get => _ProductName;
            set => Set(ref _ProductName, value);
        }
        #endregion

        #region Товары->Форма->Бренд товара
        private string _ProductBrand;
        public string ProductBrand
        {
            get => _ProductBrand;
            set => Set(ref _ProductBrand, value);
        }
        #endregion

        #region Товары->Форма->Тип товара
        private string _ProductType;
        public string ProductType
        {
            get => _ProductType;
            set => Set(ref _ProductType, value);
        }
        #endregion

        #region Товары->Форма->Цвет товара
        private string _ProductColor;
        public string ProductColor
        {
            get => _ProductColor;
            set => Set(ref _ProductColor, value);
        }
        #endregion

        #region Товары->Форма->Размер товара
        private string _ProductSize;
        public string ProductSize
        {
            get => _ProductSize;
            set => Set(ref _ProductSize, value);
        }
        #endregion

        #region Товары->Форма->Цена товара
        private string _ProductPrice;
        public string ProductPrice
        {
            get => _ProductPrice;
            set => Set(ref _ProductPrice, value);
        }
        #endregion

        #region Товары->Форма->Количество товара
        private string _ProductAmount;
        public string ProductAmount
        {
            get => _ProductAmount;
            set => Set(ref _ProductAmount, value);
        }
        #endregion

        private Visibility _TabControlForProductsVisibility = Visibility.Collapsed;

        public Visibility TabControlForProductsVisibility
        { 
            get => _TabControlForProductsVisibility; 
            set => Set(ref _TabControlForProductsVisibility, value); 
        }
        #endregion

        #region Команды

        #region Команда "Открыть меню Товары"
        public ICommand OpenProductsMenuWindow { get; }
        private bool IsProductsMenuOpen { get; set; } = false;
        public void OnOpenProductsMenuWindowExecute(object p)
        {
            TabControlForProductsVisibility = Visibility.Visible;
        }

        public bool CanOpenProductsMenuWindowExecute(object p)
        {
            if (!IsProductsMenuOpen)
                return true;
            else
                return false;
        }
        #endregion
        #endregion
        public MainWindowViewModel()
        {
            OpenProductsMenuWindow = new LambdaCommand(OnOpenProductsMenuWindowExecute, CanOpenProductsMenuWindowExecute);
        }
    }
}
