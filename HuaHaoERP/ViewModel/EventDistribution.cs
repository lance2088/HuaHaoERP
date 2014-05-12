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
                CustomerEvent.OnUpdateDataGrid();
            };
            CustomerEvent.EUpdate += (sender, e) =>
            {
                new ViewModel.Customer.CustomerConsole().Update(e.CustomerData);
                CustomerEvent.OnUpdateDataGrid();
            };
            CustomerEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.Customer.CustomerConsole().MarkDelete(e.CustomerData);
                CustomerEvent.OnUpdateDataGrid();
            };
            //外加工商
            ProcessorsEvent.EAdd += (sender, e) =>
            {
                new ViewModel.Customer.ProcessorsConsole().Add(e.ProcessorsData);
                ProcessorsEvent.OnUpdateDataGrid();
            };
            ProcessorsEvent.EUpdate += (sender, e) =>
            {
                new ViewModel.Customer.ProcessorsConsole().Update(e.ProcessorsData);
                ProcessorsEvent.OnUpdateDataGrid();
            };
            ProcessorsEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.Customer.ProcessorsConsole().MarkDelete(e.ProcessorsData);
                ProcessorsEvent.OnUpdateDataGrid();
            };
            //员工
            StaffEvent.EAdd += (sender, e) =>
            {
                new ViewModel.Customer.StaffConsole().Add(e.StaffData);
                StaffEvent.OnUpdateDataGrid();
            };
            StaffEvent.EUpdate += (sender, e) =>
            {
                new ViewModel.Customer.StaffConsole().Update(e.StaffData);
                StaffEvent.OnUpdateDataGrid();
            };
            StaffEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.Customer.StaffConsole().MarkDelete(e.StaffData);
                StaffEvent.OnUpdateDataGrid();
            };
            //供应商
            SupplierEvent.EAdd += (sender, e) =>
            {
                new ViewModel.Customer.SupplierConsole().Add(e.SupplierData);
                SupplierEvent.OnUpdateDataGrid();
            };
            SupplierEvent.EUpdate += (sender, e) =>
            {
                new ViewModel.Customer.SupplierConsole().Update(e.SupplierData);
                SupplierEvent.OnUpdateDataGrid();
            };
            SupplierEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.Customer.SupplierConsole().MarkDelete(e.SupplierData);
                SupplierEvent.OnUpdateDataGrid();
            };
            #endregion

            #region 生产资料
            //产品
            ProductEvent.EAdd += (sender, e) =>
            {
                new ViewModel.MeansOfProduction.ProductConsole().Add(e.ProductData);
                ProductEvent.OnUpdateDataGrid();
            };
            ProductEvent.EUpdate += (sender, e) =>
            {
                new ViewModel.MeansOfProduction.ProductConsole().Update(e.ProductData);
                ProductEvent.OnUpdateDataGrid();
            };
            ProductEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.MeansOfProduction.ProductConsole().MarkDelete(e.ProductData);
                ProductEvent.OnUpdateDataGrid();
            };
            //原材料
            RawMaterialsEvent.EAdd += (sender, e) =>
            {
                new ViewModel.MeansOfProduction.RawMaterialsConsole().Add(e.RawMaterialsData);
                RawMaterialsEvent.OnUpdateDataGrid();
            };
            RawMaterialsEvent.EUpdate += (sender, e) =>
            {
                new ViewModel.MeansOfProduction.RawMaterialsConsole().Update(e.RawMaterialsData);
                RawMaterialsEvent.OnUpdateDataGrid();
            };
            RawMaterialsEvent.EMarkDelete += (sender, e) =>
            {
                new ViewModel.MeansOfProduction.RawMaterialsConsole().MarkDelete(e.RawMaterialsData);
                RawMaterialsEvent.OnUpdateDataGrid();
            };
            #endregion

            #region 订单
            ProductOrderEvent.EAdd += (s, e) =>
            {
                new ViewModel.Orders.ProductOrderConsole().Add(e.Data);
                ProductOrderEvent.OnUpdateDataGrid();
            };
            ProductOrderEvent.EUpdate += (s, e) =>
            {
                new ViewModel.Orders.ProductOrderConsole().Update(e.Data);
                ProductOrderEvent.OnUpdateDataGrid();
            };
            ProductOrderEvent.EMarkDelete += (s, e) =>
            {
                new ViewModel.Orders.ProductOrderConsole().MarkDelete(e.Data);
                ProductOrderEvent.OnUpdateDataGrid();
            };
            #endregion
        }
    }
}
