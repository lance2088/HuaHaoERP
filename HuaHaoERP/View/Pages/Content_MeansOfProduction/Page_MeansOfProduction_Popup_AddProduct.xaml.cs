using HuaHaoERP.Helper.Events;
using System;
using System.Windows;
using System.Windows.Controls;

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
            this.TextBox_Number.Focus();
        }
        public Page_MeansOfProduction_Popup_AddProduct(object data)
        {
            InitializeComponent();
            isNew = false;
            InitializeData((Model.ProductModel)data);
            this.TextBox_Number.Focus();
        }
        private void InitializeData()
        {
            this.ComboBox_P1.ItemsSource = Helper.DataDefinition.Process.ProcessList;
            this.ComboBox_P1.SelectedIndex = 1;
            this.ComboBox_P2.ItemsSource = Helper.DataDefinition.Process.ProcessList;
            this.ComboBox_P2.SelectedIndex = 2;
            this.ComboBox_P3.ItemsSource = Helper.DataDefinition.Process.ProcessList;
            this.ComboBox_P3.SelectedIndex = 3;
            this.ComboBox_P4.ItemsSource = Helper.DataDefinition.Process.ProcessList;
            this.ComboBox_P4.SelectedIndex = 4;
            this.ComboBox_P5.ItemsSource = Helper.DataDefinition.Process.ProcessList;
            this.ComboBox_P5.SelectedIndex = 5;
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
            this.ComboBox_Material.Text = d.Material;
            this.ComboBox_Type.Text = d.Type;
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
            d.Material = this.ComboBox_Material.Text.Trim();
            d.Type = this.ComboBox_Type.Text.Trim();
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
            ProductEvent.OnUpdateDataGrid();
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndGetData())
            {
                if (isNew)
                {
                    if(!new ViewModel.MeansOfProduction.ProductConsole().Add(d))
                    {
                        MessageBox.Show("编号或名称重复","错误");
                        return;
                    }
                    StatusBarMessageEvent.OnUpdateMessage("添加产品：" + d.Name);
                }
                else
                {
                    if(!new ViewModel.MeansOfProduction.ProductConsole().Update(d))
                    {
                        MessageBox.Show("编号或名称重复", "错误");
                        return;
                    }
                    StatusBarMessageEvent.OnUpdateMessage("修改产品：" + d.Name);
                }
                Button_Cancel_Click(null, null);
            }
            else
            {
                MessageBox.Show("请检查输入是否有误。", "错误");
            }
        }

        private void EditableComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).IsDropDownOpen = true;
        }
    }
}
