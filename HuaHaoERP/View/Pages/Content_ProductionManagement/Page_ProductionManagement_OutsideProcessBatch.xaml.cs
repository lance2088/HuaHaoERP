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
using System.Collections.ObjectModel;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    /// <summary>
    /// 批量导入导出外加工单Page
    /// </summary>
    public partial class Page_ProductionManagement_OutsideProcessBatch : Page
    {
        bool isOut = false;

        public Page_ProductionManagement_OutsideProcessBatch(bool Out)
        {
            this.isOut = Out;
            InitializeComponent();
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }
    }
}
