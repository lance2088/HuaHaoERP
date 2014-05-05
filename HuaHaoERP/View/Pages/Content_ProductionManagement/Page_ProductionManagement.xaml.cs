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

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionManagement : Page
    {
        public Page_ProductionManagement()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeData();
        }

        private void InitializeData()
        {
            this.DatePicker_外加工.SelectedDate = DateTime.Now;
            Button_Click(null,null);
            Button_Click(null,null);
        }
        private void AddAssemblyLineModule()
        {
            Grid g = new Grid();
            g.Height = 400;
            g.Width = 300;
            g.Background = new SolidColorBrush(Colors.LightBlue);
            this.WrapPanel_AssemblyLine.Children.Add(g);
            //this.WrapPanel_AssemblyLine.RegisterName("gridggg", g);
            Frame f = new Frame();
            f.Content = new Page_ProductionManagement_AssemblyLineModule();
            g.Children.Add(f);
        }
        private void RemoveAssemblyLineModule()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddAssemblyLineModule();
        }
    }
}
