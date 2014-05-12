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
            InitializeProductDataGrid();
            InitializeRawMaterialsDataGrid();
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
            List<Model.ProductModel> data;
            new ViewModel.MeansOfProduction.ProductConsole().ReadList(out data);
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
                Helper.Events.ProductEvent.OnMarkDelete(this, data);
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
                Helper.Events.RawMaterialsEvent.OnMarkDelete(this, data);
            }
        }
        #endregion
    }
}
