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
        private string company;
        private string address;
        private string phone;
        private string mobilePhone;
        private string fax;
        private string business;
        private string remark;
        private string lastOrderTime;
        private int customerLevel;
        private int orderQuantity;
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
        public string Company
        {
            get { return company; }
            set { company = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
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
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        public string LastOrderTime
        {
            get { return lastOrderTime; }
            set { lastOrderTime = value; }
        }
        public int CustomerLevel
        {
            get { return customerLevel; }
            set { customerLevel = value; }
        }
        public int OrderQuantity
        {
            get { return orderQuantity; }
            set { orderQuantity = value; }
        }
        public string DeleteMark
        {
            get { return deleteMark; }
            set { deleteMark = value; }
        }
        #endregion
    }
}
