using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model
{
    class CustomerModel
    {
        private Guid guid;

        internal Guid Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        private string number;

        internal string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string name;

        internal string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string company;

        internal string Company
        {
            get { return company; }
            set { company = value; }
        }
        private string address;

        internal string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string phone;

        internal string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private string mobilePhone;

        internal string MobilePhone
        {
            get { return mobilePhone; }
            set { mobilePhone = value; }
        }
        private string fax;

        internal string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        private string business;

        internal string Business
        {
            get { return business; }
            set { business = value; }
        }
        private string remark;

        internal string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private DateTime lastOrderTime;

        internal DateTime LastOrderTime
        {
            get { return lastOrderTime; }
            set { lastOrderTime = value; }
        }
        private int customerLevel;

        internal int CustomerLevel
        {
            get { return customerLevel; }
            set { customerLevel = value; }
        }
        private int orderQuantity;

        internal int OrderQuantity
        {
            get { return orderQuantity; }
            set { orderQuantity = value; }
        }
    }
}
