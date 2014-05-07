using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model
{
    class ProcessorsModel
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
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string area;

        public string Area
        {
            get { return area; }
            set { area = value; }
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
        private string clerk;

        public string Clerk
        {
            get { return clerk; }
            set { clerk = value; }
        }
        private string openingBank;

        public string OpeningBank
        {
            get { return openingBank; }
            set { openingBank = value; }
        }
        private int bankCardNo;

        public int BankCardNo
        {
            get { return bankCardNo; }
            set { bankCardNo = value; }
        }
        private string bankCardName;

        public string BankCardName
        {
            get { return bankCardName; }
            set { bankCardName = value; }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private DateTime deleteMark;

        public DateTime DeleteMark
        {
            get { return deleteMark; }
            set { deleteMark = value; }
        }
        private DateTime addTime;

        public DateTime AddTime
        {
            get { return addTime; }
            set { addTime = value; }
        }
    }
}
