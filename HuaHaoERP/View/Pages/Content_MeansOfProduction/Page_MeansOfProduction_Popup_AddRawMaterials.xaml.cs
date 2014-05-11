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
using System.Data;

namespace HuaHaoERP.View.Pages.Content_MeansOfProduction
{
    public partial class Page_MeansOfProduction_Popup_AddRawMaterials : Page
    {
        private Model.RawMaterialsModel d = new Model.RawMaterialsModel();
        private Guid Guid;
        private Guid OldGuid;
        private string OldAddTime = "";
        private bool isNew = true;

        public Page_MeansOfProduction_Popup_AddRawMaterials()
        {
            InitializeComponent();
            InitializeData();
        }
        public Page_MeansOfProduction_Popup_AddRawMaterials(object data)
        {
            InitializeComponent();
            isNew = false;
            InitializeData((Model.RawMaterialsModel)data);
        }
        private void InitializeData()
        {
            this.ComboBox_Supplier.ItemsSource = Helper.DataDefinition.CustomerLibrary.SupplierList.DefaultView;
            this.ComboBox_Supplier.DisplayMemberPath = "Name";
            this.ComboBox_Supplier.SelectedValuePath = "GUID";//GUID四个字母要大写
            //this.ComboBox_Supplier.SelectedIndex = 0;
        }
        private void InitializeData(Model.RawMaterialsModel d)
        {
            InitializeData();
            this.d = d;
            OldGuid = d.Guid;
            this.TextBox_Number.Text = d.Number;
            this.TextBox_Name.Text = d.Name;
            this.TextBox_Weight.Text = d.Weight;
            this.TextBox_Material.Text = d.Material;
            this.ComboBox_Supplier.Text = d.SupplierName;
            this.ComboBox_Sp1.Text = d.Sp1;
            this.ComboBox_Sp2.Text = d.Sp2;
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
            d.Weight = this.TextBox_Weight.Text.Trim();
            d.Material = this.TextBox_Material.Text.Trim();
            if (this.ComboBox_Supplier.Text != "")
            {
                d.Supplier = (Guid)this.ComboBox_Supplier.SelectedValue;
            }
            d.Sp1 = this.ComboBox_Sp1.Text;
            d.Sp2 = this.ComboBox_Sp2.Text;
            d.Remark = this.TextBox_Remark.Text.Trim();
            if (OldAddTime == "")
            {
                d.AddTime = DateTime.Now;
            }
            return flag;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            PopUpEvent.OnHidePopUp(this);
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndGetData())
            {
                if(isNew)
                {
                    StatusBarMessageEvent.OnUpdateMessage(this, "添加原材料：" + d.Name);
                }
                else
                {
                    Model.RawMaterialsModel dOld = new Model.RawMaterialsModel();
                    dOld.Guid = OldGuid;
                    RawMaterialsEvent.OnDelete(this, dOld);
                    StatusBarMessageEvent.OnUpdateMessage(this, "修改原材料：" + d.Name);
                }
                RawMaterialsEvent.OnAdd(this, d);
                Button_Cancel_Click(null, null);
            }
            else
            {
                StatusBarMessageEvent.OnUpdateMessage(this, "添加/修改原材料失败");
            }
        }

        private void Button_ClearComboBoxSupplier_Click(object sender, RoutedEventArgs e)
        {
            this.ComboBox_Supplier.SelectedIndex = -1;
        }
    }
}
