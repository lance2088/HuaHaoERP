using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using f = System.Windows.Forms;

namespace HuaHaoERP.View.Pages.Content_Settings
{
    public partial class Page_数据备份 : Page
    {

        public Page_数据备份()
        {
            InitializeComponent();
            if (new Helper.SettingFile.INIHelper().IniReadValue("Database", "BackupPATH").Length == 0)
            {
                new Helper.SettingFile.INIHelper().IniWriteValue("Database", "BackupPATH", AppDomain.CurrentDomain.BaseDirectory + "DataBackup");
            }
            this.TextBox_BackupPath.Text = new Helper.SettingFile.INIHelper().IniReadValue("Database", "BackupPATH");
            if (new Helper.SettingFile.INIHelper().IniReadValue("Database", "自动备份") == "True")
            {
                this.CheckBox_自动备份.IsChecked = true;
            }
            if (IsNumber(new Helper.SettingFile.INIHelper().IniReadValue("Database", "备份天数")))
            {
                this.TextBox_备份天数.Text = new Helper.SettingFile.INIHelper().IniReadValue("Database", "备份天数");
            }
        }

        public bool IsNumber(String strNumber)
        {
            return Regex.IsMatch(strNumber, @"^[+-]?\d*$");
        }

        private void CheckBox_自动备份_Click(object sender, RoutedEventArgs e)
        {
            bool check = (bool)this.CheckBox_自动备份.IsChecked;
            new Helper.SettingFile.INIHelper().IniWriteValue("Database", "自动备份", check.ToString());
        }

        private void Button_浏览路径_Click(object sender, RoutedEventArgs e)
        {
            f.FolderBrowserDialog fb = new f.FolderBrowserDialog();
            if (fb.ShowDialog() == f.DialogResult.OK)
            {
                this.TextBox_BackupPath.Text = fb.SelectedPath;
                new Helper.SettingFile.INIHelper().IniWriteValue("Database", "BackupPATH", fb.SelectedPath);
            }
        }

        private void Button_浏览文件_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            open.Title = "请选择要导入的数据库文件";
            open.Filter = "数据库文件|*.db";
            open.RestoreDirectory = true;
            if ((bool)open.ShowDialog().GetValueOrDefault())
            {
                this.TextBox_恢复文件.Text = open.FileName;
            }
        }

        private void Button_备份_Click(object sender, RoutedEventArgs e)
        {
            if (Backup(true))
            {
                MessageBox.Show("备份成功。", "信息");
            }
        }

        private void Button_恢复_Click(object sender, RoutedEventArgs e)
        {
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "Data//Data.db", AppDomain.CurrentDomain.BaseDirectory + "Data//TempData.db", true);
            File.Copy(this.TextBox_恢复文件.Text, AppDomain.CurrentDomain.BaseDirectory + "Data//Data.db", true);
            MessageBox.Show("恢复成功，请重新打开软件");
            Application.Current.Shutdown();
        }

        private void Button_保存_Click(object sender, RoutedEventArgs e)
        {

        }

        internal bool Backup(bool 强制备份 = false)
        {
            bool 要不要备份 = 强制备份;
            if (!强制备份)
            {
                if (new Helper.SettingFile.INIHelper().IniReadValue("Database", "自动备份") == "True")
                {
                    if (new Helper.SettingFile.INIHelper().IniReadValue("Database", "备份天数").Length > 0)
                    {
                        int 备份天数 = int.Parse(new Helper.SettingFile.INIHelper().IniReadValue("Database", "备份天数"));
                        if (备份天数 > 0)
                        {
                            if (new Helper.SettingFile.INIHelper().IniReadValue("Database", "上次备份时间").Length == 0)
                            {
                                要不要备份 = true;
                            }
                            else
                            {
                                DateTime 上次备份时间 = DateTime.Parse(new Helper.SettingFile.INIHelper().IniReadValue("Database", "上次备份时间"));
                                if ((DateTime.Now - 上次备份时间).Days >= 备份天数)
                                {
                                    要不要备份 = true;
                                }
                            }
                        }
                    }
                }
            }
            if (要不要备份)
            {
                if (!Directory.Exists(new Helper.SettingFile.INIHelper().IniReadValue("Database", "BackupPATH")))
                {
                    new Helper.SettingFile.INIHelper().IniWriteValue("Database", "BackupPATH", AppDomain.CurrentDomain.BaseDirectory + "DataBackup");
                }
                new Helper.SQLite.DBHelper().Backup(new Helper.SettingFile.INIHelper().IniReadValue("Database", "BackupPATH") + "\\Backup" + DateTime.Now.ToString("yyyyMMddHHmmss") + Helper.DataDefinition.CommonParameters.DbPassword + ".db");
                new Helper.SettingFile.INIHelper().IniWriteValue("Database", "上次备份时间", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            return true;
        }

        private void TextBox_备份天数_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = this.TextBox_备份天数.Text.Trim();
            if (IsNumber(text))
            {
                new Helper.SettingFile.INIHelper().IniWriteValue("Database", "备份天数", text);
            }
        }
    }
}
