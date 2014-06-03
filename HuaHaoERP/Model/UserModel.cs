using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model
{
    class UserModel
    {
        private int id;
        private int rowid;
        private string username;
        private string password;
        private string realname;
        private string displayPermissions;
        private int permissions;

        public int Permissions
        {
            get { return permissions; }
            set { permissions = value; }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public string DisplayPermissions
        {
            get { return displayPermissions; }
            set { displayPermissions = value; }
        }


        public string Realname
        {
            get { return realname; }
            set { realname = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public int Rowid
        {
            get { return rowid; }
            set { rowid = value; }
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
