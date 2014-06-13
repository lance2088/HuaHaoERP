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
        bool isSelectAllDefault = false;

        public Page_ProductionManagement_ChooseProduct()
        {
            InitializeComponent();
            this.ComboBox_ProductType.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductTypeListWithAll;
            this.ComboBox_ProductType.SelectedIndex = 0;
            InitializeData();
        }

        private void InitializeData()
        {
            string Screening = this.ComboBox_Screening.Text;
            string ProductType = this.ComboBox_ProductType.Text;
            new ViewModel.MeansOfProduction.ProductConsole().ReadList(ProductType, Screening, out d);
            foreach(Model.ProductModel dm in d)
            {
                if(Helper.DataDefinition.CommonParameters.AssemblyLineModuleShow.Contains(dm.Guid))
                {
                    dm.IsShow = true;
                }
            }
            this.DataGrid_Product.ItemsSource = d;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.AssemblyLineEvent.OnShowAssemblyLineModule(d);
            new Helper.SettingFile.AssemblyLineModule().Clear();
            foreach(Model.ProductModel dm in d)
            {
                if (dm.IsShow == true)
                {
                    new Helper.SettingFile.AssemblyLineModule().Write(dm.Guid.ToString());
                }
            }
            Button_Cancel_Click(null, null);
        }

        private void Button_SelectAll_Click(object sender, RoutedEventArgs e)
        {
            if (isSelectAllDefault)
            {
                foreach (Model.ProductModel md in d)
                {
                    md.IsShow = false;
                }
                isSelectAllDefault = false;
                this.Button_SelectAll.Content = "全选";
            }
            else
            {
                foreach (Model.ProductModel md in d)
                {
                    md.IsShow = true;
                }
                isSelectAllDefault = true;
                this.Button_SelectAll.Content = "清空";
            }
        }

        private void Button_Anti_electionDefault_Click(object sender, RoutedEventArgs e)
        {
            foreach (Model.ProductModel md in d)
            {
                md.IsShow = !md.IsShow;
            }
        }

        private void ComboBox_ProductType_DropDownClosed(object sender, EventArgs e)
        {
            InitializeData();
        }

        private void ComboBox_Screening_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ComboBox_Screening.IsDropDownOpen = true;
        }

        private void ComboBox_Screening_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            InitializeData();
        }

        private void DataGridCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Product.SelectedCells.Count > 0)
            {
                Model.ProductModel TempD = this.DataGrid_Product.SelectedCells[0].Item as Model.ProductModel;
                TempD.IsShow = !TempD.IsShow;
            }
        }
    }
}
