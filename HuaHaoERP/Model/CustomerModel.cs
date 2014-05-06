using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model
{
    class CustomerModel
    {
        private Guid guid;
        private int id;
        private string number;
        private string name;
        private string address;
        private string area;
        private string phone;
        private string mobilePhone;
        private string fax;
        private string business;
        private string clerk;
        private decimal debtCeiling;
        private string remark;
        private string deleteMark;

        #region GETSET
        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Area
        {
            get { return area; }
            set { area = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string MobilePhone
        {
            get { return mobilePhone; }
            set { mobilePhone = value; }
        }
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        public string Business
        {
            get { return business; }
            set { business = value; }
        }
        public string Clerk
        {
            get { return clerk; }
            set { clerk = value; }
        }
        /// <summary>
        /// 欠款上限
        /// </summary>
        public decimal DebtCeiling
        {
            get { return debtCeiling; }
            set { debtCeiling = value; }
        }
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        public string DeleteMark
        {
            get { return deleteMark; }
            set { deleteMark = value; }
        }
        #endregion
    }
}
