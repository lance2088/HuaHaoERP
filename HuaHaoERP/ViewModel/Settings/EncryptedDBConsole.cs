﻿
namespace HuaHaoERP.ViewModel.Settings
{
    class EncryptedDBConsole
    {
        internal void Encrypted(string NewPassword)
        {
            new Helper.SQLite.DBHelper().ChangeDBPassword(NewPassword);
            new Helper.SettingFile.DatabaseEncryption().Write(NewPassword);
            Helper.DataDefinition.CommonParameters.DbPassword = NewPassword;
        }

        internal void Decryption()
        {
            new Helper.SettingFile.DatabaseEncryption().Clear();
            new Helper.SQLite.DBHelper().ClearDBPassword();
            Helper.DataDefinition.CommonParameters.DbPassword = "";
        }
    }
}
