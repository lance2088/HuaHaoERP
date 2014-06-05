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

namespace HuaHaoERP.View.Pages.Content_Others
{
    public partial class Page_About : Page
    {
        private string FileName;

        public Page_About()
        {
            InitializeComponent();
            this.Label_Version.Content += Application.ResourceAssembly.GetName().Version.ToString();
        }

        private void Button_ImportLicense_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            open.Title = "请选择要导入的License文件";
            open.Filter = "石蚁License文件|*.license";
            open.RestoreDirectory = true;
            if ((bool)open.ShowDialog().GetValueOrDefault())
            {
                FileName = open.FileName;

            }
        }
    }
}
