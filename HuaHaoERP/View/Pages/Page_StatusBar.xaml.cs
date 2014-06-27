using HuaHaoERP.Helper.Events;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HuaHaoERP.View.Pages
{
    public partial class Page_StatusBar : Page
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private double ShowSeconds = 0;

        public Page_StatusBar()
        {
            InitializeComponent();
            InitializeData();
            SubscribeToEvent();
        }

        private void InitializeData()
        {
            this.Label_Imprint.Content = "Copyright © 2014 StoneAnt. All Rights Reserved";
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(0.1);//设置刷新的间隔时间
            timer.Start();
        }

        private void SubscribeToEvent()
        {
            StatusBarMessageEvent.EUpdateMessage += Label_Message_UpdateData;
        }
        private void Label_Message_UpdateData(object sender, StatusBarMessageEventArgs e)
        {
            this.Label_Message.Content = e.Message;
            ShowSeconds = 5;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Label_DateTime.Content = DateTime.Now.ToString("yyyy年MM月dd日 dddd HH:mm:ss");
            if (ShowSeconds > 0)
            {
                ShowSeconds -= 0.1;
            }
            else if (ShowSeconds > -1)
            {
                this.Label_Message.Content = "";
                ShowSeconds = -1;
            }
        }
    }
}
