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

namespace HuaHaoERP.View.Pages.Content2
{
    public partial class Page_MainContent2 : Page
    {
        public Page_MainContent2()
        {
            InitializeComponent();
        }

        #region Product 产品

        private void Button_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            PopUpEventArgs MyE = new PopUpEventArgs();
            MyE.ClassObject = new HuaHaoERP.View.Pages.Content2.Page_MainContent2_Popup_AddProduct();
            PopUpEvent.OnShowPopUp(this, MyE);
        }

        #endregion

        #region SemifinishedProduct 半成品

        private void Button_AddSemifinishedProduct_Click(object sender, RoutedEventArgs e)
        {
            PopUpEventArgs MyE = new PopUpEventArgs();
            MyE.ClassObject = new HuaHaoERP.View.Pages.Content2.Page_MainContent2_Popup_SemifinishedProduct();
            PopUpEvent.OnShowPopUp(this, MyE);
        }

        #endregion

        #region RawMaterials 原料

        private void Button_AddRawMaterials_Click(object sender, RoutedEventArgs e)
        {
            PopUpEventArgs MyE = new PopUpEventArgs();
            MyE.ClassObject = new HuaHaoERP.View.Pages.Content2.Page_MainContent2_Popup_RawMaterials();
            PopUpEvent.OnShowPopUp(this, MyE);
        }

        #endregion

        

        

        
    }
}
