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
    public partial class Page_ProductionManagement : Page
    {
        public Page_ProductionManagement()
        {
            InitializeComponent();
            SubscribeToEvent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeData();
        }
        private void SubscribeToEvent()
        {
            AssemblyLineEvent.EShowAssemblyLineModule += (s, e) =>
            {
                foreach(Model.ProductModel d in e.ProductData)
                {
                    if(d.IsShow == true)
                    {
                        AddAssemblyLineModule(d.Guid);
                    }
                }
            };
            AssemblyLineEvent.ERemoveAssemblyLineModule += (s, e) =>
            {
                RemoveAssemblyLineModule(e.RegisterName);
            };
        }
        private void InitializeData()
        {
            this.DatePicker_Processors.SelectedDate = DateTime.Now;
            this.ComboBox_Product_Out.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductList.DefaultView;
            this.ComboBox_Product_Out.DisplayMemberPath = "Name";
            this.ComboBox_Product_Out.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Product_Out.SelectedIndex = 0;
            this.ComboBox_Processors_Out.ItemsSource = Helper.DataDefinition.ComboBoxList.ProcessorsList.DefaultView;
            this.ComboBox_Processors_Out.DisplayMemberPath = "Name";
            this.ComboBox_Processors_Out.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Processors_Out.SelectedIndex = 0;
            this.ComboBox_Product_In.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductList.DefaultView;
            this.ComboBox_Product_In.DisplayMemberPath = "Name";
            this.ComboBox_Product_In.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Product_In.SelectedIndex = 0;
            this.ComboBox_Processors_In.ItemsSource = Helper.DataDefinition.ComboBoxList.ProcessorsList.DefaultView;
            this.ComboBox_Processors_In.DisplayMemberPath = "Name";
            this.ComboBox_Processors_In.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Processors_In.SelectedIndex = 0;
        }
        /// <summary>
        /// 添加流水线模块
        /// </summary>
        private void AddAssemblyLineModule(Guid ProductGuid)
        {
            string RegisterName = "Grid_" + ProductGuid.ToString().Replace("-", "");
            if (this.WrapPanel_AssemblyLine.FindName(RegisterName) as Grid != null)
            {
                return;
            }
            Grid g = new Grid();
            this.WrapPanel_AssemblyLine.Children.Add(g);
            this.WrapPanel_AssemblyLine.RegisterName(RegisterName, g);
            Frame f = new Frame();
            f.Content = new Page_ProductionManagement_AssemblyLineModule(RegisterName, ProductGuid);
            g.Children.Add(f);
        }
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
    }
}
