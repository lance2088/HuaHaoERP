using HuaHaoERP.Model.ProductionManagement;
using HuaHaoERP.ViewModel.ProductionManagement;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionBookkeeping : Page
    {
        ObservableCollection<Model_ProductionBookkeeping> data = new ObservableCollection<Model_ProductionBookkeeping>();
        string oldValue;
        Guid newProductGuid = new Guid();

        public Page_ProductionBookkeeping()
        {
            InitializeComponent();
            if (!Properties.Settings.Default.isShowDiffColumn)
            {
                HideDiff();
            }
            InitializeData();
        }

        private void InitializeData()
        {
            this.DataGrid_ProductionBookkeeping.ItemsSource = null;
            System.Threading.Thread.Sleep(100);
            if (new ProductionBookkeepingConsole().ReadData(out data))
            {
                this.DataGrid_ProductionBookkeeping.ItemsSource = data;
            }
        }

        private void DataGrid_ProductionBookkeeping_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            oldValue = (e.Column.GetCellContent(e.Row) as TextBlock).Text; ;
        }

        private void DataGrid_ProductionBookkeeping_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            string newValue = (e.EditingElement as TextBox).Text.Trim();
            if (newValue != oldValue)
            {
                string Header = e.Column.Header.ToString();
                Model_ProductionBookkeeping m = this.DataGrid_ProductionBookkeeping.SelectedCells[0].Item as Model_ProductionBookkeeping;
                Model_ProductionBookkeeping mdata = data[data.IndexOf(m)];
                switch (Header)
                {
                    case "编号":

                        break;
                    case "工序1":
                        mdata.P1Num = int.Parse(newValue);
                        mdata.P1Diff = mdata.P2Num - mdata.P1Num;
                        break;
                    case "工序2":
                        mdata.P2Num = int.Parse(newValue);
                        mdata.P1Diff = mdata.P2Num - mdata.P1Num;
                        mdata.P2Diff = mdata.P3Num - mdata.P2Num;
                        break;
                    case "工序3":
                        mdata.P3Num = int.Parse(newValue);
                        mdata.P2Diff = mdata.P3Num - mdata.P2Num;
                        mdata.P3Diff = mdata.P4Num - mdata.P3Num;
                        break;
                    case "工序4":
                        mdata.P4Num = int.Parse(newValue);
                        mdata.P3Diff = mdata.P4Num - mdata.P3Num;
                        break;
                    case "备注":
                        mdata.Remark = newValue;
                        break;
                }
                new ProductionBookkeepingConsole().Update(mdata);
            }
        }

        private void Button_New_Click(object sender, RoutedEventArgs e)
        {
            if (newProductGuid != new Guid())
            {
                if (new ProductionBookkeepingConsole().Add(newProductGuid))
                {
                    InitializeData();
                }
            }
            newProductGuid = new Guid();
            this.TextBox_NewProduct.Text = "";
        }

        private void TextBox_NewProduct_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.TextBox_NewProduct.Text.Length > 0)
                {
                    Model_Product m = new ProductionBookkeepingConsole().ReadProduct(this.TextBox_NewProduct.Text);
                    if (m.Guid != new Guid())
                    {
                        this.newProductGuid = m.Guid;
                        this.TextBox_NewProduct.Text = m.Name;
                    }
                    else
                    {
                        this.newProductGuid = new Guid();
                        this.TextBox_NewProduct.Text = "";
                    }
                }
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_ProductionBookkeeping.SelectedCells.Count > 0)
            {
                if (MessageBox.Show("确认删除记录？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Model_ProductionBookkeeping m = this.DataGrid_ProductionBookkeeping.SelectedCells[0].Item as Model_ProductionBookkeeping;
                    if (new ProductionBookkeepingConsole().Delete(m.Guid))
                    {
                        InitializeData();
                    }
                }
            }
        }

        private void Button_ShowDiff_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.isShowDiffColumn)
            {
                HideDiff();
                Properties.Settings.Default.isShowDiffColumn = false;
            }
            else
            {
                ShowDiff();
                Properties.Settings.Default.isShowDiffColumn = true;
            }
        }

        private void ShowDiff()
        {
            this.DataGridTextColumn_Diff1.Visibility = System.Windows.Visibility.Visible;
            this.DataGridTextColumn_Diff2.Visibility = System.Windows.Visibility.Visible;
            this.DataGridTextColumn_Diff3.Visibility = System.Windows.Visibility.Visible;
            this.Button_ShowDiff.Content = "隐藏差额";
        }
        private void HideDiff()
        {
            this.DataGridTextColumn_Diff1.Visibility = System.Windows.Visibility.Collapsed;
            this.DataGridTextColumn_Diff2.Visibility = System.Windows.Visibility.Collapsed;
            this.DataGridTextColumn_Diff3.Visibility = System.Windows.Visibility.Collapsed;
            this.Button_ShowDiff.Content = "显示差额";
        }
    }
}
