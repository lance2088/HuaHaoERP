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
                CustomerEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            CustomerEvent.EDelete += (sender, e) =>
            {
                new ViewModel.Customer.CustomerConsole().Delete(e.CustomerData);
                CustomerEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            CustomerEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.Customer.CustomerConsole().MarkDelete(e.CustomerData);
                CustomerEvent.OnUpdateDataGrid(sender, new EventArgs());
            };

            ProcessorsEvent.EAdd += (sender, e) =>
            {
                new ViewModel.Customer.ProcessorsConsole().Add(e.ProcessorsData);
                ProcessorsEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            ProcessorsEvent.EDelete += (sender, e) =>
            {
                new ViewModel.Customer.ProcessorsConsole().Delete(e.ProcessorsData);
                ProcessorsEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            ProcessorsEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.Customer.ProcessorsConsole().MarkDelete(e.ProcessorsData);
                ProcessorsEvent.OnUpdateDataGrid(sender, new EventArgs());
            };

            StaffEvent.EAdd += (sender, e) =>
            {
                new ViewModel.Customer.StaffConsole().Add(e.StaffData);
                StaffEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            StaffEvent.EDelete += (sender, e) =>
            {
                new ViewModel.Customer.StaffConsole().Delete(e.StaffData);
                StaffEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            StaffEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.Customer.StaffConsole().MarkDelete(e.StaffData);
                StaffEvent.OnUpdateDataGrid(sender, new EventArgs());
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
