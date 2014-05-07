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
        internal static void InitEvent()
        {
            CustomerEvent.EAdd += (sender, e) =>
            {
                new ViewModel.Customer.CustomerConsole().Add(e.CustomerData);
            };
            CustomerEvent.EDelete += (sender, e) =>
            {
                new ViewModel.Customer.CustomerConsole().Delete(e.CustomerData);
            };
            CustomerEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.Customer.CustomerConsole().MarkDelete(e.CustomerData);
            };

            ProcessorsEvent.EAdd += (sender, e) =>
            {
                new ViewModel.Customer.ProcessorsConsole().Add(e.ProcessorsData);
            };
            ProcessorsEvent.EDelete += (sender, e) =>
            {
                new ViewModel.Customer.ProcessorsConsole().Delete(e.ProcessorsData);
            };
            ProcessorsEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.Customer.ProcessorsConsole().MarkDelete(e.ProcessorsData);
            };

            StaffEvent.EAdd += (sender, e) =>
            {
                new ViewModel.Customer.StaffConsole().Add(e.StaffData);
            };
            StaffEvent.EDelete += (sender, e) =>
            {
                new ViewModel.Customer.StaffConsole().Delete(e.StaffData);
            };
            StaffEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.Customer.StaffConsole().MarkDelete(e.StaffData);
            };

            SupplierEvent.EAdd += (sender, e) =>
            {
                new ViewModel.Customer.SupplierConsole().Add(e.SupplierData);
            };
            SupplierEvent.EDelete += (sender, e) =>
            {
                new ViewModel.Customer.SupplierConsole().Delete(e.SupplierData);
            };
            SupplierEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.Customer.SupplierConsole().MarkDelete(e.SupplierData);
            };
        }
    }
}
