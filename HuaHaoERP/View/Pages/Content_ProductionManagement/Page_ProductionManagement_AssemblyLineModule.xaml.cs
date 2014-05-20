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
using HuaHaoERP.Helper.Events;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionManagement_AssemblyLineModule : Page
    {
        private string GridName;
        private Guid ProductGuid;
        private Model.AssemblyLineModuleModel d;

        public Page_ProductionManagement_AssemblyLineModule(string Name, Guid ProductGuid)
        {
            InitializeComponent();
            this.GridName = Name;
            this.ProductGuid = ProductGuid;
            InitializeData();
        }
        private void InitializeData()
        {
            this.DataGrid.ItemsSource = null;
            if(new ViewModel.ProductionManagement.AssemblyLineModuleConsole().ReadList(ProductGuid, out d))
            {
                this.Label_ProductName.Content = d.Name;
                this.DataGrid.ItemsSource = d.ProcessList;
            }
        }
        private void InitializeStaffComboBox()
        {
            this.ComboBox_StaffList.ItemsSource = Helper.DataDefinition.ComboBoxList.StaffList.DefaultView;
            this.ComboBox_StaffList.DisplayMemberPath = "Name";
            this.ComboBox_StaffList.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_StaffList.SelectedIndex = 0;
        }
        /// <summary>
        /// 加/减工序的半成品数量
        /// parm=1 加， -1减
        /// </summary>
        /// <param name="parm"></param>
        private void ChangeQuantity(int parm)
        {
            if (this.DataGrid.SelectedCells.Count > 0)
            {
                Model.AssemblyLineModuleProcessModel dp = this.DataGrid.SelectedCells[0].Item as Model.AssemblyLineModuleProcessModel;
                string pro = dp.Process;
                int Quantity = 0;
                if (int.TryParse(this.TextBox_Quantity.Text, out Quantity))
                {
                    dp.Guid = Guid.NewGuid();
                    dp.StaffID = (Guid)this.ComboBox_StaffList.SelectedValue;
                    dp.ProductID = this.ProductGuid;
                    dp.Quantity = parm * Quantity;
                    if(new ViewModel.ProductionManagement.AssemblyLineModuleConsole().Add(dp))
                    {
                        InitializeData();
                    }
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
            ChangeQuantity(1);
        }

        private void Button_Reduce_Click(object sender, RoutedEventArgs e)
        {
            ChangeQuantity(-1);
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (this.DataGrid.SelectedCells.Count > 0)
            {
                Model.AssemblyLineModuleProcessModel dp = this.DataGrid.SelectedCells[0].Item as Model.AssemblyLineModuleProcessModel;
                this.Label_Process.Content = dp.Process;
            }
        }

    }
}
