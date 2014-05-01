using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model
{
    class CustomerModel
    {
        private Guid guid;

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string company;

        public string Company
        {
            get { return company; }
            set { company = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private string mobilePhone;

        public string MobilePhone
        {
            get { return mobilePhone; }
            set { mobilePhone = value; }
        }
        private string fax;

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        private string business;

        public string Business
        {
            get { return business; }
            set { business = value; }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private string lastOrderTime;

        public string LastOrderTime
        {
            get { return lastOrderTime; }
            set { lastOrderTime = value; }
        }
        private int customerLevel;

        public int CustomerLevel
        {
            get { return customerLevel; }
            set { customerLevel = value; }
        }
        private int orderQuantity;

        public int OrderQuantity
        {
            get { return orderQuantity; }
            set { orderQuantity = value; }
        }

        private string deleteMark;

        public string DeleteMark
        {
            get { return deleteMark; }
            set { deleteMark = value; }
        }
    }
}
