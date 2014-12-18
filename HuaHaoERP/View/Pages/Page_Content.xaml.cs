using System.Windows.Controls;

namespace HuaHaoERP.View.Pages
{
    public partial class Page_Content : Page
    {
        public Page_Content()
        {
            InitializeComponent();
            InitializeData();
            this.Frame_业务管理.Content = new View.Pages.业务管理.Page_业务管理();
        }

        private void InitializeData()
        {
            if (Helper.DataDefinition.CommonParameters.IsLockApp)
            {
                this.TabItem_CustomerLibrary.Visibility = System.Windows.Visibility.Collapsed;//屏蔽客户库
                this.TabItem_MeansOfProduction.Visibility = System.Windows.Visibility.Collapsed;//屏蔽资料库
                this.TabItem_ProductionManagement.Visibility = System.Windows.Visibility.Collapsed;//屏蔽生产管理
                this.TabItem_Warehouse.Visibility = System.Windows.Visibility.Collapsed;//屏蔽仓库管理
                this.TabItem_Settings.IsSelected = true;
            }
            else
            {
                int Permissions = Helper.DataDefinition.CommonParameters.Permissions;
                if (Permissions < 8)
                {
                    if (Permissions == 0)//仓管
                    {
                        this.Frame_Content_Warehouse.Content = new Content_Warehouse.Page_Warehouse();//实例化仓库
                        this.TabItem_ProductionManagement.Visibility = System.Windows.Visibility.Collapsed;//屏蔽生产管理
                        this.TabItem_Warehouse.IsSelected = true;
                    }
                    else if (Permissions == 1)//流水线管理
                    {
                        this.Frame_Content_ProductionManagement.Content = new Content_ProductionManagement.Page_ProductionManagement();//实例化生产管理
                        this.TabItem_Warehouse.Visibility = System.Windows.Visibility.Collapsed;//屏蔽仓库管理
                        this.TabItem_ProductionManagement.IsSelected = true;
                    }
                    else if (Permissions == 2)//流水线管理+仓管
                    {
                        this.Frame_Content_ProductionManagement.Content = new Content_ProductionManagement.Page_ProductionManagement();//实例化生产管理
                        this.Frame_Content_Warehouse.Content = new Content_Warehouse.Page_Warehouse();//实例化仓库
                    }
                }
                else//全权限
                {
                    this.Frame_Content_ProductionManagement.Content = new Content_ProductionManagement.Page_ProductionManagement();//实例化生产管理
                    this.Frame_Content_Warehouse.Content = new Content_Warehouse.Page_Warehouse();//实例化仓库
                }
                //不论权限
                this.Frame_Content_CustomerLibrary.Content = new Content_CustomerLibrary.Page_CustomerLibrary();//实例化客户库
                this.Frame_Content_MeansOfProduction.Content = new Content_MeansOfProduction.Page_MeansOfProduction();//实例化生产资料
                this.Frame_Content_Statements.Content = new Content_Statements.Page_Statements();//实例化报表
            }
            this.Frame_Content_Settings.Content = new Content_Settings.Page_Settings();//实例化设置
        }

    }
}
