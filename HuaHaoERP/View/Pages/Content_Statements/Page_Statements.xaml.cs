using System.Windows.Controls;

namespace HuaHaoERP.View.Pages.Content_Statements
{
    public partial class Page_Statements : Page
    {
        public Page_Statements()
        {
            InitializeComponent();
            this.Frame_Production.Content = new Page_Statements_Production();
        }
    }
}
