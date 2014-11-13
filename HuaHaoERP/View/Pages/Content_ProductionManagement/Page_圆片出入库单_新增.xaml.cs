using System;
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
            InitData();
        }

        private void InitData()
        {
            if (_inOut == 1)
            {
                this.Label_Title.Content = "圆片入库单";
            }
            else
            {
                this.Label_Title.Content = "圆片出库单";
            }
            this.DatePicker_InsertDate.SelectedDate = DateTime.Now;
            this.TextBox_Number.Text = "SYP000001";
        }

        private bool Commit()
        {



            return false;
        }

        private void Button_Cancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }

        private void Button_CommitNew_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Commit())
            {
                Helper.Events.PopUpEvent.OnShowPopUp(new Page_圆片出入库单_新增(_inOut));
            }
        }

        private void Button_Commit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Commit())
            {
                Helper.Events.PopUpEvent.OnHidePopUp();
            }
        }
    }
}
