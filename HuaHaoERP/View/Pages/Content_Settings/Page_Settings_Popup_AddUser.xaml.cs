using HuaHaoERP.Helper.Events.UpdateEvent;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HuaHaoERP.View.Pages.Content_Settings
{
    public partial class Page_Settings_Popup_AddUser : Page
    {
        private Model.UserModel d = new Model.UserModel();
        private ViewModel.Settings.UserConsole uc = new ViewModel.Settings.UserConsole();
        private bool isNew = true;
        public Page_Settings_Popup_AddUser()
        {
            InitializeComponent();
            InitComboBox();
        }
        public Page_Settings_Popup_AddUser(object data)
        {
            InitializeComponent();
            InitComboBox();
            isNew = false;
            InitializeData((Model.UserModel)data);
        }

        private void InitComboBox()
        {
            ComboBox_用户权限.ItemsSource = uc.GetComboBoxPermissions();
            ComboBox_用户权限.DisplayMemberPath = "DisplayPermissions";
            ComboBox_用户权限.SelectedValuePath = "Permissions";
            ComboBox_用户权限.SelectedIndex = 0;
        }

        private void InitializeData(Model.UserModel d)
        {
            this.d = d;
            TextBox_用户名.IsReadOnly = true;
            TextBox_用户名.Text = d.Username;
            TextBox_用户密码.Password = d.Password;
            TextBox_真实姓名.Text = d.Realname;
            ComboBox_用户权限.SelectedValue = d.Permissions;
            TextBox_Remark.Text = d.Remark;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndGetData())
            {
                if (!isNew)
                {
                    uc.Update(d);
                    UserEvent.OnUpdateDataGrid();
                    Helper.Events.StatusBarMessageEvent.OnUpdateMessage("修改用户：" + d.Username);
                }
                else
                {
                    uc.Add(d);
                    UserEvent.OnUpdateDataGrid();
                    Helper.Events.StatusBarMessageEvent.OnUpdateMessage("添加用户：" + d.Username);
                }
                Button_Cancel_Click(null, null);
            }
        }

        private bool CheckAndGetData()
        {
            bool flag = true;
            string username = TextBox_用户名.Text.Trim();
            if (ComboBox_用户权限.SelectedIndex == 0)
            {
                TextBlock_Permissions.Text = "请选择用户权限";
                return false;
            }
            if (!ValidateUserName())
            {
                return false;
            }
            if (!ValidatePassword())
            {
                return false;
            }
            d.Username = TextBox_用户名.Text;
            d.Password = Helper.Tools.TranslatePassword.TranslateToString(TextBox_用户密码.SecurePassword);
            d.Permissions = Int32.Parse(ComboBox_用户权限.SelectedValue.ToString());
            d.Realname = TextBox_真实姓名.Text;
            d.Remark = this.TextBox_Remark.Text.Trim();
            return flag;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }


        private bool ValidateUserName()
        {
            bool flag = true;
            if (string.IsNullOrEmpty(TextBox_用户名.Text.Trim()))
            {
                TextBlock_用户名.Text = "请填写用户名";
                flag = false;
            }
            else if (uc.ValidateUserName(TextBox_用户名.Text) && isNew) 
            {
                TextBlock_用户名.Text = "当前用户名已存在，请勿重复添加！";
                flag = false;
            }
            if(flag)
            {
                TextBlock_用户名.Text = "";
            }
            return flag;
        }

        private void TextBox_用户名_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ValidateUserName();
        }

        private bool ValidatePassword()
        {
            if (TextBox_用户密码.SecurePassword.Length == 0)
            {
                TextBlock_密码.Text = "密码不能为空";
                return false;
            }
            else
            {
                TextBlock_密码.Text = "";
            }
            return true;
        }
        private void TextBox_用户密码_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!ValidatePassword())
            {
                TextBox_用户密码.Focus();
            }
        }

    }
}
