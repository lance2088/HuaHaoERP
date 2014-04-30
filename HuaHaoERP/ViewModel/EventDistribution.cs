using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Helper.Events;

namespace HuaHaoERP.ViewModel
{
    /// <summary>
    /// 事件分发
    /// </summary>
    static class EventDistribution
    {
        static EventDistribution()
        {
            CustomerEvent.EAdd += (sender, e) => 
            {
                new ViewModel.Customer.CustomerConsole().Add(e.CustomerData);
            };
            CustomerEvent.EDelete += (sender, e) =>
            {
                new ViewModel.Customer.CustomerConsole().Delete(e.CustomerData);
            };
            CustomerEvent.EModify += (sender, e) => 
            {
                new ViewModel.Customer.CustomerConsole().Modify(e.CustomerData);
            };
        }

        //private static void Customer_Add(object sender, CustomerEventArgs e)
        //{
        //    new ViewModel.Customer.CustomerConsole().Add(e.CustomerData);
        //}
        //private static void Customer_Delete(object sender, CustomerEventArgs e)
        //{
        //    new ViewModel.Customer.CustomerConsole().Delete(e.CustomerData);
        //}
        //private static void Customer_Modify(object sender, CustomerEventArgs e)
        //{
        //    new ViewModel.Customer.CustomerConsole().Modify(e.CustomerData);
        //}
    }
}
