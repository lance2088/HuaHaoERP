using HuaHaoERP.Helper.Events;
using HuaHaoERP.Helper.Events.MeansOfProduction;
using HuaHaoERP.Model.MeansOfProduction;
using System.Windows;
using System.Windows.Controls;

namespace HuaHaoERP.View.Pages.Content_MeansOfProduction
{
    public partial class Page_新增圆片资料 : Page
    {
        public Page_新增圆片资料()
        {
            InitializeComponent();
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            if (string.IsNullOrWhiteSpace(this.TextBox_编号.Text))
            {
                message += "请输入编号";
            }
            if (string.IsNullOrWhiteSpace(this.TextBox_直径.Text) || string.IsNullOrWhiteSpace(this.TextBox_厚度.Text))
            {
                message += "、直径、厚度";
            }
            if (!string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show(message);
                return;
            }
            Model_圆片资料 m = new Model_圆片资料();
            m.编号 = this.TextBox_编号.Text.Trim();
            m.直径 = this.TextBox_直径.Text.Trim();
            m.厚度 = this.TextBox_厚度.Text.Trim();
            m.备注 = this.TextBox_备注.Text.Trim();
            if (new ViewModel.MeansOfProduction.Vm_圆片().Add(m))
            {
                Event_圆片.OnUpdate圆片资料();
                PopUpEvent.OnHidePopUp();
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            PopUpEvent.OnHidePopUp();
        }
    }
}
