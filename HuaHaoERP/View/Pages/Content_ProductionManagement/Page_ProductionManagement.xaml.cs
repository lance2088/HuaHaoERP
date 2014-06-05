﻿using System;
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

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionManagement : Page
    {
        private string CountOutOrder;
        private string CountInOrder;

        private DateTime ProcessorsFirst;
        private DateTime ProcessorsEnd;
        private Guid ProductID;
        private Guid ProcessorsID;

        public Page_ProductionManagement()
        {
            InitializeComponent();
            SubscribeToEvent();
            InitializeData();
        }

        private void SubscribeToEvent()
        {
            AssemblyLineEvent.EShowAssemblyLineModule += (s, e) =>
            {
                foreach (Model.ProductModel d in e.ProductData)
                {
                    if(d.IsShow == true)
                    {
                        AddAssemblyLineModule(d.Guid);
                    }
                    else
                    {
                        RemoveAssemblyLineModule("Grid_ALM_" + d.Guid.ToString().Replace("-", ""));
                    }
                }
            };
            AssemblyLineEvent.ERemoveAssemblyLineModule += (s, e) =>
            {
                RemoveAssemblyLineModule(e.RegisterName);
            };
            ProductionManagement_AssemblyLineEvent.EUpdateDataGrid += (s, e) =>
            {
                InitializeOutsideProcessDataGrid();
                foreach (Guid id in Helper.DataDefinition.CommonParameters.AssemblyLineModuleShow)
                {
                    AddAssemblyLineModule(id);
                }
            };
        }
        private void InitializeData()
        {
            foreach(Guid str in Helper.DataDefinition.CommonParameters.AssemblyLineModuleShow)
            {
                AddAssemblyLineModule(str);
            }
            this.DatePicker_ProcessorsFirst.SelectedDate = DateTime.Now.Date;
            this.DatePicker_ProcessorsEnd.SelectedDate = DateTime.Now.Date;
            InitProductComboBox();
            InitProcessorsComboBox();
            InitializeOutsideProcessDataGrid();
            Button_Reflash_Click(null,null);
        }
        private void InitProductComboBox()
        {
            this.ComboBox_Product.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductListWithAll.DefaultView;
            this.ComboBox_Product.DisplayMemberPath = "Name";
            this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Product.SelectedIndex = 0;
        }
        private void InitProcessorsComboBox()
        {
            this.ComboBox_Processors.ItemsSource = Helper.DataDefinition.ComboBoxList.ProcessorsListWithAll.DefaultView;
            this.ComboBox_Processors.DisplayMemberPath = "Name";
            this.ComboBox_Processors.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Processors.SelectedIndex = 0;
        }

        /// <summary>
        /// 添加流水线模块
        /// </summary>
        /// <param name="ProductGuid"></param>
        private void AddAssemblyLineModule(Guid ProductGuid)
        {
            string RegisterName = "Grid_ALM_" + ProductGuid.ToString().Replace("-", "");
            if (this.WrapPanel_AssemblyLine.FindName(RegisterName) as Grid != null)
            {
                RemoveAssemblyLineModule(RegisterName);
            }
            Grid g = new Grid();
            this.WrapPanel_AssemblyLine.Children.Add(g);
            this.WrapPanel_AssemblyLine.RegisterName(RegisterName, g);
            Frame f = new Frame();
            f.FocusVisualStyle = null;
            f.Content = new Page_ProductionManagement_AssemblyLineModule(RegisterName, ProductGuid);
            g.Children.Add(f);
        }
        /// <summary>
        /// 删除流水线模块
        /// </summary>
        /// <param name="RegisterName"></param>
        private void RemoveAssemblyLineModule(string RegisterName)
        {
            Grid g = this.WrapPanel_AssemblyLine.FindName(RegisterName) as Grid;
            if(g != null)
            {
                this.WrapPanel_AssemblyLine.Children.Remove(g);
                this.WrapPanel_AssemblyLine.UnregisterName(RegisterName);
            }
        }

        private void Button_ChooseProduct_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_ProductionManagement_ChooseProduct());
        }

        private void Button_AddProcessOut_Click(object sender, RoutedEventArgs e)
        {
            PopUpEvent.OnShowPopUp(new Page_ProductionManagement_OutsideProcess(true));
        }

        private void Button_AddProcessIn_Click(object sender, RoutedEventArgs e)
        {
            PopUpEvent.OnShowPopUp(new Page_ProductionManagement_OutsideProcess(false));
        }

        private void InitializeOutsideProcessDataGrid()
        {
            if (this.ComboBox_Processors.SelectedValue == null)
            {
                return;
            }
            this.ProcessorsID = (Guid)this.ComboBox_Processors.SelectedValue;
            if (this.ComboBox_Product.SelectedValue == null)
            {
                return;
            }
            this.ProductID = (Guid)this.ComboBox_Product.SelectedValue;
            this.ProcessorsFirst = (DateTime)this.DatePicker_ProcessorsFirst.SelectedDate;
            this.ProcessorsEnd = ((DateTime)this.DatePicker_ProcessorsEnd.SelectedDate).AddDays(1);

            List<Model.ProductionManagement_OutsideProcessModel> data;
            new ViewModel.ProductionManagement.OutsideProcessConsole().ReadList("出单", ProcessorsFirst, ProcessorsEnd, ProductID, ProcessorsID, out data, out CountOutOrder);
            this.DataGrid_ProcessOut.ItemsSource = data;
            new ViewModel.ProductionManagement.OutsideProcessConsole().ReadList("入单", ProcessorsFirst, ProcessorsEnd, ProductID, ProcessorsID, out data, out CountInOrder);
            this.DataGrid_ProcessIn.ItemsSource = data;

            this.Label_CountOutOrder.Content = this.CountOutOrder;
            this.Label_CountInOrder.Content = this.CountInOrder;
        }

        private void ComboBox_Processors_DropDownClosed(object sender, EventArgs e)
        {
            InitializeOutsideProcessDataGrid();
        }

        private void ComboBox_Product_DropDownClosed(object sender, EventArgs e)
        {
            InitializeOutsideProcessDataGrid();
        }

        private void DatePicker_ProcessorsEnd_CalendarClosed(object sender, RoutedEventArgs e)
        {
            InitializeOutsideProcessDataGrid();
        }

        private void DatePicker_ProcessorsFirst_CalendarClosed(object sender, RoutedEventArgs e)
        {
            InitializeOutsideProcessDataGrid();
        }

        private void Button_Today_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker_ProcessorsFirst.SelectedDate = DateTime.Now.Date;
            this.DatePicker_ProcessorsEnd.SelectedDate = DateTime.Now.Date;
            InitializeOutsideProcessDataGrid();
        }

        private void Button_AllDate_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker_ProcessorsFirst.SelectedDate = Convert.ToDateTime("2010-01-01 00:00:00");
            this.DatePicker_ProcessorsEnd.SelectedDate = Convert.ToDateTime("2024-01-01 00:00:00");
            InitializeOutsideProcessDataGrid();
        }

        private void Button_AssemblyLineHistory_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_ProductionManagement_AssemblyLineModuleDetails());
        }

        private void MenuItem_dgmenu1_Del_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_ProcessOut.SelectedCells.Count > 0)
            {
                Model.ProductionManagement_OutsideProcessModel d = this.DataGrid_ProcessOut.SelectedCells[0].Item as Model.ProductionManagement_OutsideProcessModel;
                if (MessageBox.Show("确认删除出单：" + d.Id + "？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new ViewModel.ProductionManagement.OutsideProcessConsole().Delete(d.Guid);
                    InitializeOutsideProcessDataGrid();
                }
            }
        }

        private void MenuItem_dgmenu2_Del_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_ProcessIn.SelectedCells.Count > 0)
            {
                Model.ProductionManagement_OutsideProcessModel d = this.DataGrid_ProcessIn.SelectedCells[0].Item as Model.ProductionManagement_OutsideProcessModel;
                if (MessageBox.Show("确认删除入单：" + d.Id + "？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new ViewModel.ProductionManagement.OutsideProcessConsole().Delete(d.Guid);
                    InitializeOutsideProcessDataGrid();
                }
            }
        }

        #region 生产统计
        private void InitializeAssemblyLineDetailsDataGrid()
        {
            List<Model.ProductionManagement.AssemblyLineDetailsListModel> d = new List<Model.ProductionManagement.AssemblyLineDetailsListModel>();
            if(new ViewModel.ProductionManagement.AssemblyLineDetailsConsole().ReadList(out d))
            {
                this.DataGrid_AssemblyLineDetails.ItemsSource = d;
            }
        }
        private void Button_Reflash_Click(object sender, RoutedEventArgs e)
        {
            InitializeAssemblyLineDetailsDataGrid();
            Helper.Events.StatusBarMessageEvent.OnUpdateMessage("刷新生产统计成功");
        }
        #endregion

        private void MenuItem_dgmenu1_InOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_ProcessOut.SelectedCells.Count > 0)
            {
                Model.ProductionManagement_OutsideProcessModel d = this.DataGrid_ProcessOut.SelectedCells[0].Item as Model.ProductionManagement_OutsideProcessModel;
                PopUpEvent.OnShowPopUp(new Page_ProductionManagement_OutsideProcess(false, d.ProductName, d.ProcessorsName));
            }
           
        }

        private void Button_ClearModule_Click(object sender, RoutedEventArgs e)
        {
            this.WrapPanel_AssemblyLine.Children.Clear();
            new Helper.SettingFile.AssemblyLineModule().Clear();
        }

        private void ComboBox_Product_KeyUp(object sender, KeyEventArgs e)
        {
            this.ComboBox_Product.IsDropDownOpen = true;
            string Parm = this.ComboBox_Product.Text;
            DataSet ds = new DataSet();
            if (new ViewModel.MeansOfProduction.ProductConsole().GetNameList(Parm, out ds))
            {
                this.ComboBox_Product.ItemsSource = ds.Tables[0].DefaultView;
                this.ComboBox_Product.DisplayMemberPath = "Name";
                this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
            }
        }

        private void ComboBox_Processors_KeyUp(object sender, KeyEventArgs e)
        {
            this.ComboBox_Processors.IsDropDownOpen = true;
            string Parm = this.ComboBox_Processors.Text;
            DataSet ds = new DataSet();
            if (new ViewModel.Customer.ProcessorsConsole().GetNameList(Parm, out ds))
            {
                this.ComboBox_Processors.ItemsSource = ds.Tables[0].DefaultView;
                this.ComboBox_Processors.DisplayMemberPath = "Name";
                this.ComboBox_Processors.SelectedValuePath = "GUID";//GUID四个字母要大写
            }
        }

        private void ComboBox_Product_DropDownOpened(object sender, EventArgs e)
        {
            if(this.ComboBox_Product.SelectedValue != null)
            {
                InitProductComboBox();
            }
        }

        private void ComboBox_Processors_DropDownOpened(object sender, EventArgs e)
        {
            if(this.ComboBox_Processors.SelectedValue != null)
            {
                InitProcessorsComboBox();
            }
        }

    }
}
