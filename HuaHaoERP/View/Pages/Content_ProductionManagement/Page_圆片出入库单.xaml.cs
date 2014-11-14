using System;
using System.Windows.Controls;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_圆片出入库单 : Page
    {
        private int _inOut = 0;//1 in 0 out

        public Page_圆片出入库单(int inOut)
        {
            InitializeComponent();
            _inOut = inOut;
            if (_inOut == 1)
            {
                this.DataGridTextColumn_入库半成品编号.Visibility = System.Windows.Visibility.Collapsed;
                this.DataGridTextColumn_半成品品名.Visibility = System.Windows.Visibility.Collapsed;
                this.DataGridTextColumn_半成品数量.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.DataGridTextColumn_损耗数量.Visibility = System.Windows.Visibility.Collapsed;
            }
            this.DatePicker_StartDate.SelectedDate = DateTime.Now.AddDays(-6).Date;
            this.DatePicker_EndDate.SelectedDate = DateTime.Now.Date;
            InitDataGrid();
            Helper.Events.MeansOfProduction.Event_圆片.EUpdate圆片订单 += (s, e) => { InitDataGrid(); };
        }

        private void InitDataGrid()
        {
            DateTime start = (DateTime)this.DatePicker_StartDate.SelectedDate;
            DateTime end = (DateTime)this.DatePicker_EndDate.SelectedDate;
            this.DataGrid_List.ItemsSource = new ViewModel.Orders.Vm_Order_圆片().ReadList(_inOut, start, end);
        }

        private void Button_NewOrder_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_圆片出入库单_新增(_inOut));
        }

        private void DatePicker_StartDate_CalendarClosed(object sender, System.Windows.RoutedEventArgs e)
        {
            InitDataGrid();
        }

        private void DatePicker_EndDate_CalendarClosed(object sender, System.Windows.RoutedEventArgs e)
        {
            InitDataGrid();
        }
    }
}
