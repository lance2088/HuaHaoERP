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

namespace HuaHaoERP.View.Pages.Content_MeansOfProduction
{
    public partial class Page_MeansOfProduction : Page
    {
        public Page_MeansOfProduction()
        {
            InitializeComponent();
        }

        #region Product 产品

        private void Button_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            PopUpEvent.OnShowPopUp(this, new HuaHaoERP.View.Pages.Content_MeansOfProduction.Page_MeansOfProduction_Popup_AddProduct());
        }

        #endregion

        #region RawMaterials 原料

        private void Button_AddRawMaterials_Click(object sender, RoutedEventArgs e)
        {
            PopUpEvent.OnShowPopUp(this, new HuaHaoERP.View.Pages.Content_MeansOfProduction.Page_MeansOfProduction_Popup_AddRawMaterials());
        }

        #endregion

    }
}
