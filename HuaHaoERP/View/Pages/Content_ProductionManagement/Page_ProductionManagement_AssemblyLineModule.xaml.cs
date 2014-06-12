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
using System.Data;
using HuaHaoERP.Helper.Events;
using HuaHaoERP.Model;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionManagement_AssemblyLineModule : Page
    {
        private string GridName;
        private Guid ProductGuid;
        private string ProductName;
        private Model.AssemblyLineModuleModel d;

        public Page_ProductionManagement_AssemblyLineModule(string Name, Guid ProductGuid)
        {
            InitializeComponent();
            FunctionalLimitation();
            this.GridName = Name;
            this.ProductGuid = ProductGuid;
            InitializeData();
        }

        /// <summary>
        /// 功能限制
        /// </summary>
        private void FunctionalLimitation()
        {
            if (Helper.DataDefinition.CommonParameters.PeriodOfValidity < 0)
            {
                this.Button_Add.IsEnabled = false;
            }
        }
        private void InitializeData()
        {
            this.DataGrid.ItemsSource = null;
            if(new ViewModel.ProductionManagement.AssemblyLineModuleConsole().ReadList(ProductGuid, out d))
            {
                ProductName = d.Name;
                this.Label_ProductName.Content = d.Name;
                this.DataGrid.ItemsSource = d.ProcessList;
                this.Label_Process.Content = "";
                this.Button_Processing.Visibility = System.Windows.Visibility.Collapsed;
                foreach(AssemblyLineModuleProcessModel dpm in d.ProcessList)
                {
                    if(dpm.Process == "抛光")
                    {
                        this.Button_Processing.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
            this.TextBox_Quantity.Clear();
            this.TextBox_Break.Clear();
        }
        private void InitializeStaffComboBox()
        {
            this.ComboBox_StaffList.ItemsSource = Helper.DataDefinition.ComboBoxList.StaffListWithoutAll.DefaultView;
            this.ComboBox_StaffList.DisplayMemberPath = "Name";
            this.ComboBox_StaffList.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_StaffList.SelectedIndex = 0;
        }
        /// <summary>
        /// 加工序的半成品数量
        /// </summary>
        private void ChangeQuantity()
        {
            if (this.DataGrid.SelectedCells.Count > 0)
            {
                int Quantity = 0;
                int Break = 0;
                if (int.TryParse(this.TextBox_Quantity.Text, out Quantity))
                {
                    if (Quantity == 0)
                    {
                        return;
                    }
                    int.TryParse(this.TextBox_Break.Text, out Break);
                    Model.AssemblyLineModuleProcessModel dp = this.DataGrid.SelectedCells[0].Item as Model.AssemblyLineModuleProcessModel;
                    dp.Guid = Guid.NewGuid();
                    if (this.ComboBox_StaffList.SelectedValue == null)
                    {
                        MessageBox.Show("请至少录入一个员工","错误");
                        return;
                    }
                    dp.StaffID = (Guid)this.ComboBox_StaffList.SelectedValue;
                    dp.ProductID = this.ProductGuid;
                    dp.Quantity = Quantity;
                    dp.BreakNum = Break;
                    if(new ViewModel.ProductionManagement.AssemblyLineModuleConsole().Add(dp))
                    {
                        InitializeData();
                    }
                }
                else
                {
                    this.TextBox_Quantity.Clear();
                    this.TextBox_Quantity.Focus();
                }
            }
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            AssemblyLineEvent.OnRemoveAssemblyLineModule(this.GridName);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeStaffComboBox();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            ChangeQuantity();
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (this.DataGrid.SelectedCells.Count > 0)
            {
                Model.AssemblyLineModuleProcessModel dp = this.DataGrid.SelectedCells[0].Item as Model.AssemblyLineModuleProcessModel;
                this.Label_Process.Content = dp.Process;
                if (dp.Process == "抛光")
                {
                    this.Button_Add.IsEnabled = false;
                }
                else
                {
                    this.Button_Add.IsEnabled = true;
                    FunctionalLimitation();
                }
            }
        }

        private void Button_Storage_Click(object sender, RoutedEventArgs e)
        {
            int Quantity = d.ProcessList[d.ProcessList.Count - 1].Quantity;
            if (Quantity > 0)
            {
                Guid StaffID = (Guid)this.ComboBox_StaffList.SelectedValue;
                string StaffName = this.ComboBox_StaffList.Text;
                string ProcessName = d.ProcessList[d.ProcessList.Count - 1].Process;
                if(new ViewModel.ProductionManagement.AssemblyLineModuleConsole().Storage(StaffID, ProductGuid, ProcessName, Quantity))
                {
                    if(new ViewModel.Warehouse.WarehouseProductConsole().Add(ProductGuid, StaffName, Quantity))
                    {
                        InitializeData();
                    }
                }
            }
        }

        private void Button_Processing_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_ProductionManagement_OutsideProcess(true, ProductName));
        }

        private void ComboBox_StaffList_KeyUp(object sender, KeyEventArgs e)
        {
            if(this.ComboBox_StaffList.SelectedValue == null)
            {
                string Parm = this.ComboBox_StaffList.Text;
                DataSet ds = new DataSet();
                if (new ViewModel.Customer.StaffConsole().GetNameList(Parm, out ds))
                {
                    this.ComboBox_StaffList.ItemsSource = ds.Tables[0].DefaultView;
                    this.ComboBox_StaffList.DisplayMemberPath = "Name";
                    this.ComboBox_StaffList.SelectedValuePath = "GUID";//GUID四个字母要大写
                }
            }
        }

        private void ComboBox_StaffList_DropDownOpened(object sender, EventArgs e)
        {
            if(this.ComboBox_StaffList.SelectedValue != null)
            {
                InitializeStaffComboBox();
            }
        }

        private void ComboBox_StaffList_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ComboBox_StaffList.IsDropDownOpen = true;
        }
    }
}
