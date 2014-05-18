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
    public partial class Page_ProductionManagement_AssemblyLineModule : Page
    {
        private string GridName;
        public Page_ProductionManagement_AssemblyLineModule(string Name)
        {
            InitializeComponent();
            this.GridName = Name;
        }
        
        private void InitializeStaffComboBox()
        {
            this.ComboBox_StaffList.ItemsSource = Helper.DataDefinition.ComboBoxList.StaffList.DefaultView;
            this.ComboBox_StaffList.DisplayMemberPath = "Name";
            this.ComboBox_StaffList.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_StaffList.SelectedIndex = 0;
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            AssemblyLineEvent.OnRemoveAssemblyLineModule(this.GridName);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeStaffComboBox();
        }
    }
}
