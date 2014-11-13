using System.Windows.Controls;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_圆片出入库单_新增 : Page
    {
        private int _inOut = 0;//1 in 0 out

        public Page_圆片出入库单_新增(int inOut)
        {
            InitializeComponent();
            _inOut = inOut;
        }
    }
}
