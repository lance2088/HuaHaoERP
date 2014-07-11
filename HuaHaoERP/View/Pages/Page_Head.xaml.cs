using System.Windows.Controls;

namespace HuaHaoERP.View.Pages
{
    public partial class Page_Head : Page
    {
        public Page_Head()
        {
            InitializeComponent();
            this.Label_LoginUser.Content += Helper.DataDefinition.CommonParameters.RealName;
            this.Label_Title.Content += " " + Helper.DataDefinition.CommonParameters.LicenseModel.Target;
        }

        private void MenuItem_1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new View.Pages.Content_ProductionManagement.Page_ProductionManagement_AssemblyLineModuleBatchInput());
        }

        private void MenuItem_2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new View.Pages.Content_ProductionManagement.Page_ProductionManagement_OutsideProcessBatch(true));
        }

        private void MenuItem_3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new View.Pages.Content_ProductionManagement.Page_ProductionManagement_OutsideProcessBatch(false));
        }

        private void MenuItem_4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new View.Pages.Content_Warehouse.Page_Warehouse_Product_BatchIn(1));
        }

        private void MenuItem_5_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new View.Pages.Content_Warehouse.Page_Warehouse_Product_BatchIn(3));
        }

        private void MenuItem_6_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new View.Pages.Content_Warehouse.Page_Warehouse_Product_BatchIn(2));
        }
    }
}
