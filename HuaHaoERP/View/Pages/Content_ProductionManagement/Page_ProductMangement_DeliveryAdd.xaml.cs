using HuaHaoERP.Helper.Events.UpdateEvent.Warehouse;
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
        ProductManagement_DevlieryModel mm = new ProductManagement_DevlieryModel();
        ObservableCollection<ProductManagement_DevlieryDetailModel> data = new ObservableCollection<ProductManagement_DevlieryDetailModel>();
        private int CellId;

        public Page_ProductMangement_DeliveryAdd()
        {
            InitializeComponent();
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
                this.DatePicker_InsertDate.SelectedDate = DateTime.Now;
                this.TextBox_Number.Text = DateTime.Now.ToString("yyyyMMdd_HHmmss");
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            #region 删除方法
            if (this.DataGrid.SelectedCells.Count != 0)
            {
                ProductManagement_DevlieryDetailModel m = DataGrid.SelectedCells[0].Item as ProductManagement_DevlieryDetailModel;
                CellId = m.Id;
                if (!m.ProductID.Equals(Guid.Empty))
                {
                    if (MessageBox.Show("是否删除此行数据?", "确认信息", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        bool flag = new DeliveryProductConsole().DeleteDetail(m);
                        if (!flag)
                        {
                            MessageBox.Show("删除不成功！未找到对应数据！");
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                m.Number = "";
                m.Name = "";
                m.QuantityA = 0;
                m.QuantityB = 0;
                m.QuantityC = 0;
            }
            #endregion
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
            if (e.Key == Key.Delete)
            {
                MenuItem_Click(this, null);
            }
        }

        private void Button_CommitNew_Click(object sender, RoutedEventArgs e)
        {
            if (Save())
            {
                InitializeDataGrid();
                IS_MODIFY = false;
                RefreshPage();
            }
        }

        private void RefreshPage()
        {
            HalfProductEvent.OnUpdateDataGrid();
            SparePartsInventoryEvent.OnUpdateDataGrid();
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if (Save())
            {
                RefreshPage();
                this.Button_Cancel_Click(this, null);
            }
            else
            {
                MessageBox.Show("操作失败，联系管理员！");
            }
        }

        private bool Save()
        {
            bool flag = true;
            if (!CheckData())
            {
                return false;
            }
            GetData();
            if (mm.Guid.Equals(Guid.Empty))
            {
                mm.Guid = Guid.NewGuid();
                flag = new DeliveryProductConsole().Insert(mm, data);
                if (!flag)
                {
                    MessageBox.Show("数据有误");
                    return false;
                }
            }
            else
            {
                flag = new DeliveryProductConsole().Update(mm, data);
                if (!flag)
                {
                    MessageBox.Show("数据有误");
                    return false;
                }
            }
            return flag;
        }

        private ProductManagement_DevlieryModel GetData()
        {
            DateTime dt = new DateTime();
            DateTime.TryParse(this.DatePicker_InsertDate.Text, out dt);
            if (DateTime.Now.Day - dt.Day > 10000)
            {
                mm.Date = DateTime.Now.ToString();
            }
            else
            {
                mm.Date = DatePicker_InsertDate.Text;
            }
            mm.OrderNO = this.TextBox_Number.Text;
            mm.Remark = this.TextBox_Remark.Text;
            mm.ProcessorID = (Guid)this.ComboBox_Processors.SelectedValue;
            return mm;
        }

        private bool CheckData()
        {
            if (ComboBox_Processors.SelectedValue == null) 
            {
                MessageBox.Show("请选择客户！");
                return false;
            }
            return true;
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
