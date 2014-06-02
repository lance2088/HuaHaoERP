using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model.ProductionManagement
{
    class AssemblyLineDetailsListModel
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
        private string productNum;

        public string ProductNumber
        {
            get { return productNum; }
            set { productNum = value; }
        }
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private string p1Num;

        public string P1Num
        {
            get { return p1Num; }
            set { p1Num = value; }
        }
        private string p2Num;

        public string P2Num
        {
            get { return p2Num; }
            set { p2Num = value; }
        }
        private string p3Num;

        public string P3Num
        {
            get { return p3Num; }
            set { p3Num = value; }
        }
        private string p4Num;

        public string P4Num
        {
            get { return p4Num; }
            set { p4Num = value; }
        }
        private string p5Num;

        public string P5Num
        {
            get { return p5Num; }
            set { p5Num = value; }
        }
        private string p6Num;

        public string P6Num
        {
            get { return p6Num; }
            set { p6Num = value; }
        }
    }
}
