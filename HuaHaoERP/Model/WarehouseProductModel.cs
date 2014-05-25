using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model
{
    class WarehouseProductModel
    {
        private int id;
        private string productID;
        private string date;
        private string optor;
        private string number;
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public string Number
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

        public string ProductID
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
