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

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionManagement_ChooseProduct : Page
    {
        List<Model.ProductModel> d;

        public Page_ProductionManagement_ChooseProduct()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            new ViewModel.MeansOfProduction.ProductConsole().ReadList(out d);
            this.DataGrid_Product.ItemsSource = d;
        }


        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.AssemblyLineEvent.OnShowAssemblyLineModule(d);
            Button_Cancel_Click(null, null);
        }

        private void Button_SelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach(Model.ProductModel md in d)
            {
                md.IsShow = true;
            }
        }

        private void Button_CLearSelected_Click(object sender, RoutedEventArgs e)
        {
            foreach (Model.ProductModel md in d)
            {
                md.IsShow = false;
            }
        }
    }
}
