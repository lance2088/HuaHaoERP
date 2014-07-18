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
        bool Is_AllDate = false;

        public Page_ProductionBookkeeping()
        {
            InitializeComponent();
            if (!Properties.Settings.Default.isShowDiffColumn)
            {
                HideDiff();
                this.CheckBox_ShowDiff.IsChecked = false;
            }
            this.DatePicker_Date.SelectedDate = DateTime.Now;
            InitializeData();
        }

        private void InitializeData()
        {
            DateTime SelectDate = (DateTime)this.DatePicker_Date.SelectedDate;
            string Product = this.TextBox_SearchProduct.Text;
            this.DataGrid_ProductionBookkeeping.ItemsSource = null;
            System.Threading.Thread.Sleep(100);
            if (Is_AllDate)
            {
                if (new ProductionBookkeepingConsole().ReadData(Product, out data))
                {
                    this.DataGrid_ProductionBookkeeping.ItemsSource = data;
                }
            }
            else
            {
                if (new ProductionBookkeepingConsole().ReadData(SelectDate.ToString("yyyy-MM-dd 00:00:00"), Product, out data))
                {
                    this.DataGrid_ProductionBookkeeping.ItemsSource = data;
                }
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
                int NewValueInt = 0;
                string Header = e.Column.Header.ToString();
                Model_ProductionBookkeeping m = this.DataGrid_ProductionBookkeeping.SelectedCells[0].Item as Model_ProductionBookkeeping;
                Model_ProductionBookkeeping mdata = data[data.IndexOf(m)];
                switch (Header)
                {
                    case "①":
                        int.TryParse(newValue, out NewValueInt);
                        mdata.P1Num = NewValueInt;
                        mdata.P1Diff = mdata.P2Num - mdata.P1Num;
                        break;
                    case "②":
                        int.TryParse(newValue, out NewValueInt);
                        mdata.P2Num = NewValueInt;
                        mdata.P1Diff = mdata.P2Num - mdata.P1Num;
                        mdata.P2Diff = mdata.P3Num - mdata.P2Num;
                        break;
                    case "③":
                        int.TryParse(newValue, out NewValueInt);
                        mdata.P3Num = NewValueInt;
                        mdata.P2Diff = mdata.P3Num - mdata.P2Num;
                        mdata.P3Diff = mdata.P4Num - mdata.P3Num;
                        break;
                    case "④":
                        int.TryParse(newValue, out NewValueInt);
                        mdata.P4Num = NewValueInt;
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
                if (this.newProductGuid == new Guid())
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
                else
                {
                    Button_New_Click(null, null);
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
                        data.Remove(m);
                    }
                }
            }
        }

        private void ShowDiff()
        {
            this.DataGridTextColumn_Diff1.Visibility = System.Windows.Visibility.Visible;
            this.DataGridTextColumn_Diff2.Visibility = System.Windows.Visibility.Visible;
            this.DataGridTextColumn_Diff3.Visibility = System.Windows.Visibility.Visible;
        }
        private void HideDiff()
        {
            this.DataGridTextColumn_Diff1.Visibility = System.Windows.Visibility.Collapsed;
            this.DataGridTextColumn_Diff2.Visibility = System.Windows.Visibility.Collapsed;
            this.DataGridTextColumn_Diff3.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void TextBox_SearchProduct_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            InitializeData();
        }

        private void CheckBox_ShowDiff_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)this.CheckBox_ShowDiff.IsChecked)
            {
                ShowDiff();
                Properties.Settings.Default.isShowDiffColumn = true;
            }
            else
            {
                HideDiff();
                Properties.Settings.Default.isShowDiffColumn = false;
            }
        }

        private void DatePicker_Date_CalendarClosed(object sender, RoutedEventArgs e)
        {
            InitializeData();
        }

        private void Button_PreviousDay_Click(object sender, RoutedEventArgs e)
        {
            DateTime DateNow = (DateTime)this.DatePicker_Date.SelectedDate;
            this.DatePicker_Date.SelectedDate = DateNow.AddDays(-1);
            InitializeData();
        }

        private void Button_NextDay_Click(object sender, RoutedEventArgs e)
        {
            DateTime DateNow = (DateTime)this.DatePicker_Date.SelectedDate;
            this.DatePicker_Date.SelectedDate = DateNow.AddDays(1);
            InitializeData();
        }

        private void Button_Today_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker_Date.SelectedDate = DateTime.Now;
            InitializeData();
        }

        private void Button_AllDay_Click(object sender, RoutedEventArgs e)
        {
            if (Is_AllDate)
            {
                this.DatePicker_Date.IsEnabled = true;
                this.Button_PreviousDay.IsEnabled = true;
                this.Button_NextDay.IsEnabled = true;
                this.Button_Today.IsEnabled = true;
                Is_AllDate = false;
            }
            else
            {
                this.DatePicker_Date.IsEnabled = false;
                this.Button_PreviousDay.IsEnabled = false;
                this.Button_NextDay.IsEnabled = false;
                this.Button_Today.IsEnabled = false;
                Is_AllDate = true;
            }
            InitializeData();
        }
    }
}
