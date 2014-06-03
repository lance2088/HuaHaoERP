using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HuaHaoERP.Helper.Events;

namespace HuaHaoERP.View.Pages
{
    public partial class Page_Content : Page
    {
        public Page_Content()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            int Permissions = Helper.DataDefinition.CommonParameters.Permissions;
            if (Permissions < 8)
            {
                this.TabItem_CustomerLibrary.Visibility = System.Windows.Visibility.Collapsed;//屏蔽客户库
                this.Frame_Content_CustomerLibrary.Content = new Content_Others.Page_InsufficientPermissions();//屏蔽客户库
                if (Permissions == 0)//仓管
                {
                    this.Frame_Content_Warehouse.Content = new Content_Warehouse.Page_Warehouse();//实例化仓库
                    this.TabItem_ProductionManagement.Visibility = System.Windows.Visibility.Collapsed;//屏蔽生产管理
                    this.Frame_Content_ProductionManagement.Content = new Content_Others.Page_InsufficientPermissions();//屏蔽生产管理
                    this.TabItem_Warehouse.IsSelected = true;
                }
                else if (Permissions == 1)//流水线管理
                {
                    this.Frame_Content_ProductionManagement.Content = new Content_ProductionManagement.Page_ProductionManagement();//实例化生产管理
                    this.TabItem_Warehouse.Visibility = System.Windows.Visibility.Collapsed;//屏蔽仓库管理
                    this.Frame_Content_Warehouse.Content = new Content_Others.Page_InsufficientPermissions();//屏蔽仓库管理
                    this.TabItem_ProductionManagement.IsSelected = true;
                }
                else if (Permissions == 2)
                {
                    this.Frame_Content_ProductionManagement.Content = new Content_ProductionManagement.Page_ProductionManagement();//实例化生产管理
                    this.Frame_Content_Warehouse.Content = new Content_Warehouse.Page_Warehouse();//实例化仓库
                }
            }
            else//全权限
            {
                this.Frame_Content_CustomerLibrary.Content = new Content_CustomerLibrary.Page_CustomerLibrary();//实例化客户库
                this.Frame_Content_ProductionManagement.Content = new Content_ProductionManagement.Page_ProductionManagement();//实例化生产管理
                this.Frame_Content_Warehouse.Content = new Content_Warehouse.Page_Warehouse();//实例化仓库
            }
            //不论权限
            this.Frame_Content_MeansOfProduction.Content = new Content_MeansOfProduction.Page_MeansOfProduction();//实例化生产资料
            this.Frame_Content_Settings.Content = new Content_Settings.Page_Settings();//实例化设置
            //this.Frame_Content_Orders.Content = new Content_Orders.Page_Orders();
            this.Frame_Content_Orders.Content = new Content_Others.Page_Expect();
        }
        
    }
}
