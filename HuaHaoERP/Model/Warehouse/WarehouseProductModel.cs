using System;

namespace HuaHaoERP.Model.Warehouse
{
    class WarehouseProductModel
    {
        private int id;
        private Guid guid;
        private Guid productID;
        private string productName;
        private string date;
        private string optor;
        private int number;
        private string remark;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public string Operator
        {
            get { return optor; }
            set { optor = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public Guid ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }

    class WarehouseProductNumModel
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private Guid productID;

        public Guid ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        public string ProductNumber { get; set; }
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        private int packageNum;

        public int PackageNum
        {
            get { return packageNum; }
            set { packageNum = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string specification;

        public string Specification
        {
            get { return specification; }
            set { specification = value; }
        }
    }

    class WarehouseProductPackingNumModel
    {

    }
}
