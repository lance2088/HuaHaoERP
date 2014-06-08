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
using System.IO;

namespace HuaHaoERP.View.Pages.Content_Others
{
    public partial class Page_About : Page
    {
        private string LicenseFile;

        public Page_About()
        {
            InitializeComponent();
            this.Label_Version.Content += Application.ResourceAssembly.GetName().Version.ToString();
            AuthorizeLabel();
        }

        private void Button_ImportLicense_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            open.Title = "请选择要导入的License文件";
            open.Filter = "石蚁License文件|*.key";
            open.RestoreDirectory = true;
            if ((bool)open.ShowDialog().GetValueOrDefault())
            {
                LicenseFile = open.FileName;
                StoneAnt.License.Model.LicenseModel m = new StoneAnt.License.Model.LicenseModel();
                if (new StoneAnt.License.Verify.Term().VerfyLicense(LicenseFile, out m))
                {

                    if(DateTime.Now.Subtract(m.CreationDate).Days > 7 || DateTime.Now.Subtract(m.CreationDate).Days < 0)
                    {
                        MessageBox.Show("许可有误或已过期，请联系开发商", "错误");
                    }
                    else
                    {
                        Helper.DataDefinition.CommonParameters.LicenseModel = m;
                        new Helper.License.FillLicense().CalculatePeriodOfValidity();
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "License.key"))
                        {
                            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "License.key", AppDomain.CurrentDomain.BaseDirectory + "License.key.bak", true);
                        }
                        try
                        {
                            File.Copy(LicenseFile, AppDomain.CurrentDomain.BaseDirectory + "License.key", true);
                        }
                        catch(Exception)
                        {
                            MessageBox.Show("注册失败，请勿重复注册", "错误");
                            return;
                        }
                        AuthorizeLabel();
                        if(new ViewModel.Security.LicenseConsole().RegisterInDB(m.Key))
                        {
                            MessageBox.Show("许可验证成功，感谢支持石蚁软件\n重启软件后生效", "通知");
                        }
                        else
                        {
                            MessageBox.Show("注册失败，请联系开发商", "错误");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("许可有误或已过期，请联系开发商", "错误");
                }
            }
        }

        private void AuthorizeLabel()
        {
            this.Label_Authorize.Content = "授权于：" + Helper.DataDefinition.CommonParameters.LicenseModel.Target
                                         + "（" + Helper.DataDefinition.CommonParameters.LicenseModel.UsersNumber + "用户）";
            if (Helper.DataDefinition.CommonParameters.LicenseModel.PeriodOfValidity == -1)
            {
                this.Label_Authorize.Content += "终身授权";
            }
            else
            {
                this.Label_Authorize.Content += "有效期：" + Helper.DataDefinition.CommonParameters.LicenseModel.PeriodOfValidity + "天"
                                              + "（剩余"+Helper.DataDefinition.CommonParameters.PeriodOfValidity+"天）";
            }
        }
    }
}
