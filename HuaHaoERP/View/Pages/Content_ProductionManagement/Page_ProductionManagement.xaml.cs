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
using HuaHaoERP.Helper.Events;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionManagement : Page
    {
        public Page_ProductionManagement()
        {
            InitializeComponent();
            SubscribeToEvent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeData();
        }
        private void SubscribeToEvent()
        {
            AssemblyLineEvent.ERemoveAssemblyLineModule += (s, e) =>
            {
                RemoveAssemblyLineModule(e.RegisterName);
            };
        }
        private void InitializeData()
        {
            this.DatePicker_外加工.SelectedDate = DateTime.Now;
        }
        private void AddAssemblyLineModule()
        {
            string RegisterName = "Grid_" + new Random().Next();
            Grid g = new Grid();
            g.Height = 300;
            g.Width = 375;
            g.Background = new SolidColorBrush(Colors.LightBlue);
            this.WrapPanel_AssemblyLine.Children.Add(g);
            this.WrapPanel_AssemblyLine.RegisterName(RegisterName, g);
            Frame f = new Frame();
            f.Content = new Page_ProductionManagement_AssemblyLineModule(RegisterName);
            g.Children.Add(f);
        }
        private void RemoveAssemblyLineModule(string RegisterName)
        {
            Grid g = this.WrapPanel_AssemblyLine.FindName(RegisterName) as Grid;
            if(g != null)
            {
                this.WrapPanel_AssemblyLine.Children.Remove(g);
                this.WrapPanel_AssemblyLine.UnregisterName(RegisterName);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddAssemblyLineModule();
        }
    }
}
