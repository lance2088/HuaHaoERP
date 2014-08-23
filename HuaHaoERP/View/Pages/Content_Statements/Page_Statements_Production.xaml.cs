using System.Windows.Controls;

namespace HuaHaoERP.View.Pages.Content_Statements
{
    public partial class Page_Statements_Production : Page
    {
        public Page_Statements_Production()
        {
            InitializeComponent();
            this.Frame_Chart.Content = new View.Pages.UserControl_Chart.UserControl_ChartTest();
        }
    }
}
