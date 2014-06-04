using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HuaHaoERP.Helper.Events;

namespace HuaHaoERP.View.Pages.Content_MeansOfProduction
{
    public partial class Page_MeansOfProduction : Page
    {
        public Page_MeansOfProduction()
        {
            InitializeComponent();
            SubscribeToEvent();
            PermissionsSettings();
            InitializeProductDataGrid();
            InitializeRawMaterialsDataGrid();
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
        }

        #region Product 产品
        private void InitializeProductDataGrid()
        {
            string ProductType = this.ComboBox_ProductType.Text;
            List<Model.ProductModel> data;
            new ViewModel.MeansOfProduction.ProductConsole().ReadList(ProductType, out data);
            this.DataGrid_Product.ItemsSource = data;
            this.ComboBox_ProductType.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductTypeListWithAll;
            this.ComboBox_ProductType.SelectedIndex = 0;
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
                if (MessageBox.Show("确认删除产品：" + data.Name + "？\n删除产品可能导致仓库中该产品无法打包和出库", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
            PopUpEvent.OnShowPopUp(new HuaHaoERP.View.Pages.Content_MeansOfProduction.Page_MeansOfProduction_Popup_AddRawMaterials());
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

        private void ComboBox_ProductType_DropDownClosed(object sender, EventArgs e)
        {
            InitializeProductDataGrid();
        }
    }
}
