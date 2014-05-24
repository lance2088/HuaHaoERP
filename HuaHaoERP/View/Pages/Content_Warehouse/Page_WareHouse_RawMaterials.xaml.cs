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
using HuaHaoERP.ViewModel.Warehouse;
using HuaHaoERP.Model;

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    public partial class Page_Warehouse_RawMaterials : Page
    {
        public Page_Warehouse_RawMaterials()
        {
            InitializeComponent();
            var rawMaterials = new List<RawMaterialsDetailModel>()
            {
                new RawMaterialsDetailModel{
                    Id = 1
                }
            };
            DataGrid_RawMaterials.ItemsSource = rawMaterials;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }
    }
}
