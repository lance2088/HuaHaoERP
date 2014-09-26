using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model.Warehouse
{
    class WarehouseHalpProductModel
    {
        private int id;
        private Guid guid;
        private Guid productID;
        private string _number;
        private string productName;
        private string date;
        private string optor;
        private string remark;
        private string _material;
        private string _type;
        private string _Specification;
        private decimal _quantity;

        public decimal Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public string Specification
        {
            get { return _Specification; }
            set { _Specification = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Material
        {
            get { return _material; }
            set { _material = value; }
        }

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

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
}
