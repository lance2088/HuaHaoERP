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
            #region 客户库
            //客户
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
            //外加工商
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
            //员工
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
            //供应商
            SupplierEvent.EAdd += (sender, e) =>
            {
                new ViewModel.Customer.SupplierConsole().Add(e.SupplierData);
                SupplierEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            SupplierEvent.EDelete += (sender, e) =>
            {
                new ViewModel.Customer.SupplierConsole().Delete(e.SupplierData);
                SupplierEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            SupplierEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.Customer.SupplierConsole().MarkDelete(e.SupplierData);
                SupplierEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            #endregion

            #region 生产资料
            //产品
            ProductEvent.EAdd += (sender, e) =>
            {
                new ViewModel.MeansOfProduction.ProductConsole().Add(e.ProductData);
                ProductEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            ProductEvent.EDelete += (sender, e) =>
            {
                new ViewModel.MeansOfProduction.ProductConsole().Delete(e.ProductData);
                ProductEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            ProductEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.MeansOfProduction.ProductConsole().MarkDelete(e.ProductData);
                ProductEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            //原材料
            RawMaterialsEvent.EAdd += (sender, e) =>
            {
                new ViewModel.MeansOfProduction.RawMaterialsConsole().Add(e.RawMaterialsData);
                RawMaterialsEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            RawMaterialsEvent.EDelete += (sender, e) =>
            {
                new ViewModel.MeansOfProduction.RawMaterialsConsole().Delete(e.RawMaterialsData);
                RawMaterialsEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            RawMaterialsEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.MeansOfProduction.RawMaterialsConsole().MarkDelete(e.RawMaterialsData);
                RawMaterialsEvent.OnUpdateDataGrid(sender, new EventArgs());
            };
            #endregion

            #region 订单
            ProductOrderEvent.EAdd += (s, e) =>
            {
                new ViewModel.Orders.ProductOrderConsole().Add(e.Data);
                ProductOrderEvent.OnUpdateDataGrid(s, new EventArgs());
            };
            ProductOrderEvent.EDelete += (s, e) =>
            {
                new ViewModel.Orders.ProductOrderConsole().Delete(e.Data);
                ProductOrderEvent.OnUpdateDataGrid(s, new EventArgs());
            };
            ProductOrderEvent.EMarkDelete += (s, e) =>
            {
                new ViewModel.Orders.ProductOrderConsole().MarkDelete(e.Data);
                ProductOrderEvent.OnUpdateDataGrid(s, new EventArgs());
            };
            #endregion
        }
    }
}
