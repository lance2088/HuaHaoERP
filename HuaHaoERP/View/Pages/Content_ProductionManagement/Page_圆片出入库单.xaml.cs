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
            InitDataGrid();
        }

        private void InitDataGrid()
        {
            this.DataGrid_List.ItemsSource = new ViewModel.Orders.Vm_Order_圆片().ReadList(_inOut);
        }

        private void Button_NewOrder_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_圆片出入库单_新增(_inOut));
        }
    }
}
