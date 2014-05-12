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
    public partial class Page_MeansOfProduction_Popup_AddProduct : Page
    {
        private Model.ProductModel d = new Model.ProductModel();
        private Guid Guid;
        private Guid OldGuid;
        private string OldAddTime = "";
        private bool isNew = true;

        public Page_MeansOfProduction_Popup_AddProduct()
        {
            InitializeComponent();
            InitializeData();
        }
        public Page_MeansOfProduction_Popup_AddProduct(object data)
        {
            InitializeComponent();
            isNew = false;
            InitializeData((Model.ProductModel)data);
        }
        private void InitializeData()
        {
            this.ComboBox_P1.ItemsSource = Helper.DataDefinition.Process.ProcessList;
            this.ComboBox_P1.SelectedIndex = 0;
            this.ComboBox_P2.ItemsSource = Helper.DataDefinition.Process.ProcessList;
            this.ComboBox_P2.SelectedIndex = 0;
            this.ComboBox_P3.ItemsSource = Helper.DataDefinition.Process.ProcessList;
            this.ComboBox_P3.SelectedIndex = 0;
            this.ComboBox_P4.ItemsSource = Helper.DataDefinition.Process.ProcessList;
            this.ComboBox_P4.SelectedIndex = 0;
            this.ComboBox_P5.ItemsSource = Helper.DataDefinition.Process.ProcessList;
            this.ComboBox_P5.SelectedIndex = 0;
            this.ComboBox_P6.ItemsSource = Helper.DataDefinition.Process.ProcessList;
            this.ComboBox_P6.SelectedIndex = 0;
        }
        private void InitializeData(Model.ProductModel d)
        {
            InitializeData();
            this.d = d;
            OldGuid = d.Guid;
            this.TextBox_Number.Text = d.Number;
            this.TextBox_Name.Text = d.Name;
            this.TextBox_Material.Text = d.Material;
            this.TextBox_Type.Text = d.Type;
            this.TextBox_Specification.Text = d.Specification;
            this.ComboBox_P1.Text = d.P1;
            this.ComboBox_P2.Text = d.P2;
            this.ComboBox_P3.Text = d.P3;
            this.ComboBox_P4.Text = d.P4;
            this.ComboBox_P5.Text = d.P5;
            this.ComboBox_P6.Text = d.P6;
            this.TextBox_PackageNumber.Text = d.PackageNumber.ToString();
            this.TextBox_Remark.Text = d.Remark;
            OldAddTime = d.AddTime.ToString();
        }
        private bool CheckAndGetData()
        {
            bool flag = true;
            if (this.TextBox_Number.Text.Trim() == "" || this.TextBox_Name.Text.Trim() == "")
            {
                return false;
            }
            if (isNew)
            {
                Guid = Guid.NewGuid();
                d.Guid = Guid;
            }
            else
            {
                d.Guid = OldGuid;
            }
            d.Number = this.TextBox_Number.Text.Trim();
            d.Name = this.TextBox_Name.Text.Trim();
            d.Material = this.TextBox_Material.Text.Trim();
            d.Type = this.TextBox_Type.Text.Trim();
            d.Specification = this.TextBox_Specification.Text.Trim();
            d.P1 = this.ComboBox_P1.Text;
            d.P2 = this.ComboBox_P2.Text;
            d.P3 = this.ComboBox_P3.Text;
            d.P4 = this.ComboBox_P4.Text;
            d.P5 = this.ComboBox_P5.Text;
            d.P6 = this.ComboBox_P6.Text;
            int PackageNumber = 0;
            flag = int.TryParse(this.TextBox_PackageNumber.Text.Trim(), out PackageNumber);
            d.PackageNumber = PackageNumber;
            d.Remark = this.TextBox_Remark.Text.Trim();
            if (OldAddTime == "")
            {
                d.AddTime = DateTime.Now;
            }
            return flag;
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            PopUpEvent.OnHidePopUp();
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndGetData())
            {
                if (isNew)
                {
                    StatusBarMessageEvent.OnUpdateMessage("添加产品：" + d.Name);
                }
                else
                {
                    Model.ProductModel dOld = new Model.ProductModel();
                    dOld.Guid = OldGuid;
                    ProductEvent.OnDelete(this, dOld);
                    StatusBarMessageEvent.OnUpdateMessage("修改产品：" + d.Name);
                }
                ProductEvent.OnAdd(this, d);
                Button_Cancel_Click(null, null);
            }
        }
    }
}
