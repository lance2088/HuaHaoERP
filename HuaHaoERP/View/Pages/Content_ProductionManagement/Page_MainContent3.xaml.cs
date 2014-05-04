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

namespace HuaHaoERP.View.Pages.Content3
{
    public partial class Page_MainContent3 : Page
    {
        public Page_MainContent3()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeData();
        }

        private void InitializeData()
        {
            this.DatePicker_版裁.SelectedDate = DateTime.Now;
            this.DatePicker_包装.SelectedDate = DateTime.Now;
            this.DatePicker_冲版.SelectedDate = DateTime.Now;
            this.DatePicker_卷边.SelectedDate = DateTime.Now;
            this.DatePicker_拉伸.SelectedDate = DateTime.Now;
            this.DatePicker_抛光.SelectedDate = DateTime.Now;
            this.DatePicker_外加工.SelectedDate = DateTime.Now;
        }
    }
}
