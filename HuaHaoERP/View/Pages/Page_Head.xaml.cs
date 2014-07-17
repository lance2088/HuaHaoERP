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
    }
}
