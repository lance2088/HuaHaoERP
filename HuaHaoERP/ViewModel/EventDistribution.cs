using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Helper.Events;
using HuaHaoERP.Helper.Events.Customer;

namespace HuaHaoERP.ViewModel
{
    /// <summary>
    /// 事件分发
    /// </summary>
    static class EventDistribution
    {
        static EventDistribution()
        {
            CustomerEvent.EAdd += Customer_Add;
        }

        private static void Customer_Add(object sender, CustomerEventArgs e)
        {
            new ViewModel.CustomerConsole().Add();
        }
    }
}
