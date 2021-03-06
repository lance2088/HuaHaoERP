﻿using HuaHaoERP.Helper.Events;
using HuaHaoERP.Helper.Events.MeansOfProduction;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace HuaHaoERP.View.Pages.Content_MeansOfProduction
{
    public partial class Page_MeansOfProduction : Page
    {
        public Page_MeansOfProduction()
        {
            InitializeComponent();
            SubscribeToEvent();
            FunctionalLimitation();
            PermissionsSettings();
            this.ComboBox_ProductType.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductTypeListWithAll;
            this.ComboBox_ProductType.SelectedIndex = 0;

            InitializeProductDataGrid();
            InitializeRawMaterialsDataGrid();
            Init圆片();
        }
        /// <summary>
        /// 功能限制
        /// </summary>
        private void FunctionalLimitation()
        {
            if (Helper.DataDefinition.CommonParameters.PeriodOfValidity < 0)
            {
                this.Button_AddProduct.IsEnabled = false;
                this.Button_AddRawMaterials.IsEnabled = false;
            }
        }

        private void PermissionsSettings()
        {
            if (Helper.DataDefinition.CommonParameters.Permissions < 8)
            {
                this.Button_DeleteProduct.IsEnabled = false;
                this.Button_DeleteRawMaterials.IsEnabled = false;
            }
        }
        private void SubscribeToEvent()
        {
            ProductEvent.EUpdateDataGrid += (s, e) =>
            {
                InitializeProductDataGrid();
            };
            RawMaterialsEvent.EUpdateDataGrid += (s, e) =>
            {
                InitializeRawMaterialsDataGrid();
            };
            Event_圆片.EUpdate圆片资料 += (s, e) => { Init圆片(); };
        }

        #region Product 产品
        private void ComboBox_ProductType_DropDownClosed(object sender, EventArgs e)
        {
            InitializeProductDataGrid();
        }
        private void InitializeProductDataGrid()
        {
            string ProductType = this.ComboBox_ProductType.Text;
            string Screening = this.TextBox_Screening.Text;
            this.ComboBox_ProductType.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductTypeListWithAll;
            this.ComboBox_ProductType.Text = ProductType;
            List<Model.ProductModel> data;
            new ViewModel.MeansOfProduction.ProductConsole().ReadList(ProductType, Screening, out data);
            this.DataGrid_Product.ItemsSource = data;
        }
        private void Button_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            PopUpEvent.OnShowPopUp(new HuaHaoERP.View.Pages.Content_MeansOfProduction.Page_MeansOfProduction_Popup_AddProduct());
        }
        private void Button_DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Product.SelectedCells.Count > 0)
            {
                HuaHaoERP.Model.ProductModel data = this.DataGrid_Product.SelectedCells[0].Item as HuaHaoERP.Model.ProductModel;
                if (MessageBox.Show("确认删除产品：" + data.Name + "？\n删除产品可能导致仓库中该产品无法打包和出库\n请谨慎操作！", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new ViewModel.MeansOfProduction.ProductConsole().MarkDelete(data);
                    Helper.Events.ProductEvent.OnUpdateDataGrid();
                }
            }
        }
        private void DataGrid_Product_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Product.SelectedCells.Count != 0)
            {
                HuaHaoERP.Model.ProductModel data = this.DataGrid_Product.SelectedCells[0].Item as HuaHaoERP.Model.ProductModel;
                Helper.Events.PopUpEvent.OnShowPopUp(new Page_MeansOfProduction_Popup_AddProduct(data));
            }
        }
        private void TextBox_Screening_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            InitializeProductDataGrid();
        }
        private void TextBox_Screening_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBox_Screening.Focus();
        }
        #endregion


        #region RawMaterials 原料
        private void InitializeRawMaterialsDataGrid()
        {
            List<Model.RawMaterialsModel> data;
            new ViewModel.MeansOfProduction.RawMaterialsConsole().ReadList(out data);
            this.DataGrid_RawMaterials.ItemsSource = data;
        }
        private void Button_AddRawMaterials_Click(object sender, RoutedEventArgs e)
        {
            PopUpEvent.OnShowPopUp(new Page_MeansOfProduction_Popup_AddRawMaterials());
        }
        private void DataGrid_RawMaterials_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_RawMaterials.SelectedCells.Count != 0)
            {
                HuaHaoERP.Model.RawMaterialsModel data = this.DataGrid_RawMaterials.SelectedCells[0].Item as HuaHaoERP.Model.RawMaterialsModel;
                Helper.Events.PopUpEvent.OnShowPopUp(new Page_MeansOfProduction_Popup_AddRawMaterials(data));
            }
        }
        private void Button_DeleteRawMaterials_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_RawMaterials.SelectedCells.Count > 0)
            {
                HuaHaoERP.Model.RawMaterialsModel data = this.DataGrid_RawMaterials.SelectedCells[0].Item as HuaHaoERP.Model.RawMaterialsModel;
                if (MessageBox.Show("确认删除原料：" + data.Name + "？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new ViewModel.MeansOfProduction.RawMaterialsConsole().MarkDelete(data);
                    Helper.Events.RawMaterialsEvent.OnUpdateDataGrid();
                }
            }
        }
        #endregion

        //
        //圆片
        //

        private void Init圆片()
        {
            this.DataGrid_圆片.ItemsSource = new ViewModel.MeansOfProduction.Vm_圆片().ReadList();
        }

        private void DataGrid_圆片_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Add圆片_Click(object sender, RoutedEventArgs e)
        {
            PopUpEvent.OnShowPopUp(new Page_新增圆片资料());
        }

        private void Button_Delete圆片_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_圆片.SelectedCells.Count > 0)
            {
                HuaHaoERP.Model.MeansOfProduction.Model_圆片资料 data = this.DataGrid_圆片.SelectedCells[0].Item as HuaHaoERP.Model.MeansOfProduction.Model_圆片资料;
                if (MessageBox.Show("确认删除圆片：" + data.编号 + "？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (new ViewModel.MeansOfProduction.Vm_圆片().Delete(data.Guid))
                    {
                        Init圆片();
                    }
                }
            }
        }
    }
}
