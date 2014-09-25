using HuaHaoERP.Model.ProductionManagement;
using HuaHaoERP.ViewModel.ProductionManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    /// <summary>
    /// Interaction logic for Page_ProductMangement_DeliveryAdd.xaml
    /// </summary>
    public partial class Page_ProductMangement_DeliveryAdd : Page
    {
        bool IS_MODIFY = false;//是否是修改模式
        ObservableCollection<ProductManagement_DevlieryDetailModel> data = new ObservableCollection<ProductManagement_DevlieryDetailModel>();

        public Page_ProductMangement_DeliveryAdd()
        {
            InitializeComponent();
            this.DatePicker_InsertDate.SelectedDate = DateTime.Now;
            this.TextBox_Number.Text = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            this.Label_Title.Content = "外加工单：抛光送货";
            InitializeDataGrid();
            InitProcessorsComboBox();
        }

        private void InitializeDataGrid()
        {
            if (IS_MODIFY)
            {
                //data = new OutsideProcessBatchInputConsole().ReadDatas(OrderData);
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    data.Add(new ProductManagement_DevlieryDetailModel { Id = i + 1 });
                }
            }
            this.DataGrid.ItemsSource = data;
        }

        private void InitProcessorsComboBox()
        {
            this.ComboBox_Processors.ItemsSource = Helper.DataDefinition.ComboBoxList.ProcessorsListWithAll.DefaultView;
            this.ComboBox_Processors.DisplayMemberPath = "Name";
            this.ComboBox_Processors.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Processors.SelectedIndex = 0;
        }

        private void ComboBox_Processors_KeyUp(object sender, KeyEventArgs e)
        {
            if ((sender as ComboBox).IsDropDownOpen == false)
            {
                (sender as ComboBox).IsDropDownOpen = true;
            }
            if (this.ComboBox_Processors.SelectedValue == null)
            {
                string Parm = this.ComboBox_Processors.Text;
                DataSet ds = new DataSet();
                if (new ViewModel.Customer.ProcessorsConsole().GetNameList(Parm, out ds))
                {
                    this.ComboBox_Processors.ItemsSource = ds.Tables[0].DefaultView;
                    this.ComboBox_Processors.DisplayMemberPath = "Name";
                    this.ComboBox_Processors.SelectedValuePath = "GUID";//GUID四个字母要大写
                }
            }
        }

        private void ComboBox_Processors_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ComboBox_Processors.IsDropDownOpen = true;
        }

        private void ComboBox_Processors_DropDownOpened(object sender, EventArgs e)
        {
            if (this.ComboBox_Processors.SelectedValue == null)
            {
                InitProcessorsComboBox();
            }
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ProductManagement_DevlieryDetailModel model = this.DataGrid.SelectedCells[0].Item as ProductManagement_DevlieryDetailModel;
            string newValue = (e.EditingElement as TextBox).Text.Trim();
            string Header = e.Column.Header.ToString();
            if (Header == "编号")
            {
                if (ComboBox_Processors.SelectedIndex == 0)
                {
                    MessageBox.Show("请选择抛光户！");
                    return;
                }
                int d = 0;
                ProductManagement_DevlieryDetailModel m = new ProductManagement_DevlieryDetailModel();
                new DeliveryProductConsole().ReadProductInfo((Guid)ComboBox_Processors.SelectedValue,newValue,out m,out d);
                DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[0]);
                data[data.IndexOf(model)].ProductID = m.ProductID;
                data[data.IndexOf(model)].Name = m.Name;
                data[data.IndexOf(model)].QuantityB = d;
            }
            else if (Header.Equals("领货数量"))
            {
                int temp = 0;
                int.TryParse(newValue, out temp);
                data[data.IndexOf(model)].QuantityC = temp + data[data.IndexOf(model)].QuantityB;
            }
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Button_CommitNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }

        private void ComboBox_Processors_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBox_Processors.SelectedValue != null)
            {
                this.DataGrid.IsEnabled = true;
            }
        }
    }
}
