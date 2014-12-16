using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace HuaHaoERP
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();
            if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
            {
                MessageBox.Show("金字塔ERP已经运行。");
                Application.Current.Shutdown();
                return;
            }
            base.OnStartup(e);
            new View.Pages.Content_Settings.Page_数据备份().Backup();
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("应用程序出现了未捕获的异常，{0}/n", e.Exception.Message);
            if (e.Exception.InnerException != null)
            {
                stringBuilder.AppendFormat("/n {0}", e.Exception.InnerException.Message);
            }
            stringBuilder.AppendFormat("/n {0}", e.Exception.StackTrace);
            MessageBox.Show("应用程序出现了未捕获的异常，请联系开发商。");
            Helper.LogHelper.FileLog.ErrorLog(stringBuilder.ToString());
            e.Handled = true;
        }
    }
}
