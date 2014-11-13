using HuaHaoERP.ViewModel.Warehouse;
using System.Windows;
using System.Windows.Controls;

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    public partial class Page_Warehouse_圆片仓库 : Page
    {
        public Page_Warehouse_圆片仓库()
        {
            InitializeComponent();
            InitDataGrid();
        }

        private void InitDataGrid()
        {
            this.DataGrid_List.ItemsSource = new Vm_Warehouse_圆片().ReadList();
        }

        private void TextBox_Search_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string search = this.TextBox_Search.Text.Trim();
            this.DataGrid_List.ItemsSource = new Vm_Warehouse_圆片().ReadList(search);
        }

        private void Button_圆片入库_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_Warehouse_圆片入库());
        }
    }
}
