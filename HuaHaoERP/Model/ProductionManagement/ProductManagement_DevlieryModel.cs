﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model.ProductionManagement
{
    class ProductManagement_DevlieryModel
    {
        private Guid _guid;
        private int _id;
        private string _orderNO;
        private Guid _processorID;
        private string _processorName;
        private string _date;
        private string _remark;
        private string _productNumber;

        public string ProductNumber
        {
            get { return _productNumber; }
            set { _productNumber = value; }
        }
        private string _productName;
        private string _quantity;

        public string Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }


        public string ProcessorName
        {
            get { return _processorName; }
            set { _processorName = value; }
        }

        public Guid ProcessorID
        {
            get { return _processorID; }
            set { _processorID = value; }
        }

        public string OrderNO
        {
            get { return _orderNO; }
            set { _orderNO = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }
    }
}
