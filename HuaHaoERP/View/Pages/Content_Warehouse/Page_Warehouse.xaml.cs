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

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    public partial class Page_Warehouse : Page
    {
        public Page_Warehouse()
        {
            InitializeComponent();
            InitPage();
        }

        private void InitPage()
        {
            #region 余料管理
            Button_Clear_Click(this, null);
            #endregion 
        }
        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            DatePicker_Date.Text = DateTime.Now.ToShortDateString();
            ComboBox_Name.ItemsSource = null;
            TextBox_Number.Text = "";
            TextBox_Remark.Text = "";
        }
    }
}
