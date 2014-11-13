using HuaHaoERP.Model.Warehouse;
using HuaHaoERP.ViewModel.Warehouse;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    public partial class Page_Warehouse_圆片仓库 : Page
    {
        private List<Model_圆片仓库> _data = new List<Model_圆片仓库>();

        public Page_Warehouse_圆片仓库()
        {
            InitializeComponent();
            InitDataGrid();
            Helper.Events.MeansOfProduction.Event_圆片.EUpdate圆片库存 += (s, e) => { InitDataGrid(); };
        }

        private void InitDataGrid()
        {
            _data = new Vm_Warehouse_圆片().ReadList();
            this.DataGrid_List.ItemsSource = _data;
        }

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchParm = this.TextBox_Search.Text.Trim();
            if (searchParm.Length == 0)
            {
                this.DataGrid_List.ItemsSource = _data;
            }
            string[] search = searchParm.Replace("，", ",").Split(',');
            IEnumerable<Model_圆片仓库> a = _data.AsEnumerable();
            foreach (string str in search)
            {
                a = from item in a
                    where item.直径.IndexOf(str) >= 0
                       || item.厚度.IndexOf(str) >= 0
                    select item;
            }
            this.DataGrid_List.ItemsSource = new List<Model_圆片仓库>(a);
        }
    }
}
