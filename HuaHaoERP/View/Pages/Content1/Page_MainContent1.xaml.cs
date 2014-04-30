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

namespace HuaHaoERP.View.Pages.Content1
{
    public partial class Page_MainContent1 : Page
    {
        public Page_MainContent1()
        {
            InitializeComponent();
        }

    #region Client

        private void Button_AddClient_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEventArgs MyE = new Helper.Events.PopUpEventArgs();
            MyE.ClassObject = new Page_MainContent1_Popup_AddClient();
            Helper.Events.PopUpEvent.OnShowPopUp(this, MyE);
        }

    #endregion

    #region Supplier

        private void Button_AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEventArgs MyE = new Helper.Events.PopUpEventArgs();
            MyE.ClassObject = new Page_MainContent1_Popup_AddSupplier();
            Helper.Events.PopUpEvent.OnShowPopUp(this, MyE);
        }

    #endregion
    }
}
