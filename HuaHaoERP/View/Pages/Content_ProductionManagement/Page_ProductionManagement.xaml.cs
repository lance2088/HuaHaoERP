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

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionManagement : Page
    {
        private int CountOutOrder;
        private int CountInOrder;

        private DateTime ProcessorsFirst;
        private DateTime ProcessorsEnd;
        private Guid ProductID;
        private Guid ProcessorsID;

        List<Model.ProductModel> data = new List<Model.ProductModel>();

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
                data = e.ProductData;
                foreach (Model.ProductModel d in data)
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
            ProductionManagement_OutsideProcessEvent.EUpdateDataGrid += (s, e) =>
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
            this.ComboBox_Product.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductListWithAll.DefaultView;
            this.ComboBox_Product.DisplayMemberPath = "Name";
            this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Product.SelectedIndex = 0;
            this.ComboBox_Processors.ItemsSource = Helper.DataDefinition.ComboBoxList.ProcessorsListWithAll.DefaultView;
            this.ComboBox_Processors.DisplayMemberPath = "Name";
            this.ComboBox_Processors.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Processors.SelectedIndex = 0;

            InitializeOutsideProcessDataGrid();
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
            this.ProcessorsID = (Guid)this.ComboBox_Processors.SelectedValue;
            this.ProductID = (Guid)this.ComboBox_Product.SelectedValue;
            this.ProcessorsEnd = (DateTime)this.DatePicker_ProcessorsEnd.SelectedDate;
            this.ProcessorsFirst = (DateTime)this.DatePicker_ProcessorsFirst.SelectedDate;

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
            this.DatePicker_ProcessorsFirst.SelectedDate = Convert.ToDateTime("2014-01-01 00:00:00");
            this.DatePicker_ProcessorsEnd.SelectedDate = Convert.ToDateTime("2024-01-01 00:00:00");
            InitializeOutsideProcessDataGrid();
        }
    }
}
