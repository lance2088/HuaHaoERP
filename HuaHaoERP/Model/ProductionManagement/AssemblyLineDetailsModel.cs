using System;

namespace HuaHaoERP.Model.ProductionManagement
{
    class AssemblyLineDetailsModel
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
        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        private Guid staffID;

        public Guid StaffID
        {
            get { return staffID; }
            set { staffID = value; }
        }
        private string staffName;

        public string StaffName
        {
            get { return staffName; }
            set { staffName = value; }
        }
        private Guid productID;

        public Guid ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private string process;

        public string Process
        {
            get { return process; }
            set { process = value; }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        private int breakNum;

        public int BreakNum
        {
            get { return breakNum; }
            set { breakNum = value; }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}
