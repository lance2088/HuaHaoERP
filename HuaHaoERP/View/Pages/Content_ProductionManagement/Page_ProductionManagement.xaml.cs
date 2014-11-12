using HuaHaoERP.Helper.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionManagement : Page
    {
        private int PageNow = 1;
        private int PageAll = 1;

        public Page_ProductionManagement()
        {
            InitializeComponent();
            SubscribeToEvent();
            InitializeData();
            FunctionalLimitation();
        }

        /// <summary>
        /// 功能限制
        /// </summary>
        private void FunctionalLimitation()
        {
            if (Helper.DataDefinition.CommonParameters.PeriodOfValidity < 0)
            {
                this.Button_BatchInputProduction.IsEnabled = false;
            }
        }

        private void SubscribeToEvent()
        {
            AssemblyLineEvent.EShowAssemblyLineModule += (s, e) =>
            {
                this.WrapPanel_AssemblyLine.Children.Clear();
                foreach (Model.ProductModel d in e.ProductData)
                {
                    if (d.IsShow == true)
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
            this.Frame_圆片入库单.Content = new Page_圆片入库单();
            this.Frame_圆片出库单.Content = new Page_圆片出库单();
            this.Frame_ProductionBookkeeping.Content = new Page_ProductionBookkeeping();
            this.Frame_Delivery.Content = new Page_ProductMangement_Delivery();
            Frame_Picking.Content = new Page_ProductMangement_Picking();
            foreach (Guid str in Helper.DataDefinition.CommonParameters.AssemblyLineModuleShow)
            {
                AddAssemblyLineModule(str);
            }
            //InitializeAssemblyLineDetailsDataGrid();
            this.ComboBox_ProductType.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductTypeListWithAll;
            this.ComboBox_ProductType.SelectedIndex = 0;
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
            if (g != null)
            {
                this.WrapPanel_AssemblyLine.Children.Remove(g);
                this.WrapPanel_AssemblyLine.UnregisterName(RegisterName);
            }
        }

        private void Button_ChooseProduct_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_ProductionManagement_ChooseProduct());
        }


        private void Button_AssemblyLineHistory_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_ProductionManagement_AssemblyLineModuleDetails());
        }


        #region 生产统计
        private void InitializeAssemblyLineDetailsDataGrid()
        {
            this.TextBox_Screening.Focus();
            string Screening = this.TextBox_Screening.Text;
            string ProductType = this.ComboBox_ProductType.Text;
            PageAll = (new ViewModel.ProductionManagement.AssemblyLineDetailsConsole().ReadCount(ProductType, Screening)) / 40 + 1;
            this.Label_Page.Content = PageNow + "/" + PageAll;
            List<Model.ProductionManagement.AssemblyLineDetailsListModel> d = new List<Model.ProductionManagement.AssemblyLineDetailsListModel>();
            if (new ViewModel.ProductionManagement.AssemblyLineDetailsConsole().ReadList(ProductType, Screening, (PageNow - 1) * 40, 40, out d))
            {
                this.DataGrid_AssemblyLineDetails.ItemsSource = d;
            }
        }

        #endregion

        private void Button_ClearModule_Click(object sender, RoutedEventArgs e)
        {
            this.WrapPanel_AssemblyLine.Children.Clear();
            new Helper.SettingFile.AssemblyLineModule().Clear();
        }

        

        private void DataGrid_AssemblyLineDetails_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeAssemblyLineDetailsDataGrid();
        }

        private void Button_PrintAssemblyLineDetails_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog Printdlg = new PrintDialog();
            Printdlg.UserPageRangeEnabled = true;
            PrintQueue pq = null;
            this.ScrollViewer_AssemblyLineDetails.ScrollToTop();
            for (int i = 1; i <= PageAll; i++)
            {
                PageNow = i;
                InitializeAssemblyLineDetailsDataGrid();
                while (pq == null)
                {
                    if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
                    {
                        pq = Printdlg.PrintQueue;
                    }
                    else
                    {
                        return;
                    }
                }
                Printdlg.PrintVisual(DataGrid_AssemblyLineDetails, "");
            }
            this.Label_Page.Content = PageNow + "/" + PageAll;
        }

        private void ComboBox_ProductType_DropDownClosed(object sender, EventArgs e)
        {
            PageNow = 1;
            InitializeAssemblyLineDetailsDataGrid();
            this.ScrollViewer_AssemblyLineDetails.ScrollToTop();
        }

        private void DataGrid_AssemblyLineDetails_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                this.ScrollViewer_AssemblyLineDetails.LineDown();
                this.ScrollViewer_AssemblyLineDetails.LineDown();
                this.ScrollViewer_AssemblyLineDetails.LineDown();
            }
            else if (e.Delta > 0)
            {
                this.ScrollViewer_AssemblyLineDetails.LineUp();
                this.ScrollViewer_AssemblyLineDetails.LineUp();
                this.ScrollViewer_AssemblyLineDetails.LineUp();
            }
        }

        private void Button_PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (PageNow > 1)
            {
                PageNow--;
                this.Label_Page.Content = PageNow + "/" + PageAll;
                InitializeAssemblyLineDetailsDataGrid();
                this.ScrollViewer_AssemblyLineDetails.ScrollToTop();
            }
        }

        private void Button_NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (PageNow < PageAll)
            {
                PageNow++;
                this.Label_Page.Content = PageNow + "/" + PageAll;
                InitializeAssemblyLineDetailsDataGrid();
                this.ScrollViewer_AssemblyLineDetails.ScrollToTop();
            }
        }

        private void TextBox_Screening_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            PageNow = 1;
            InitializeAssemblyLineDetailsDataGrid();
            this.ScrollViewer_AssemblyLineDetails.ScrollToTop();
        }
        /// <summary>
        /// 批量录入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_BatchInputProduction_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_ProductionManagement_AssemblyLineModuleBatchInput());
        }

        private void Button_BatchHistory_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Content_Warehouse.Page_Warehouse_Product_BatchHistory(1));
        }

     
        private void Button_AllInWarehouse_Click(object sender, RoutedEventArgs e)
        {
            if (new ViewModel.ProductionManagement.AssemblyLineModuleConsole().AllInStorage())
            {
                Helper.Events.ProductionManagement_AssemblyLineEvent.OnUpdateDataGrid();
                MessageBox.Show("全部入库成功。", "信息");
            }
        }

    }
}
