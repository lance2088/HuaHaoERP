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
        private int p1Num;

        public int P1Num
        {
            get { return p1Num; }
            set { p1Num = value; }
        }
        private int p2Num;

        public int P2Num
        {
            get { return p2Num; }
            set { p2Num = value; }
        }
        private int p3Num;

        public int P3Num
        {
            get { return p3Num; }
            set { p3Num = value; }
        }
        private int p4Num;

        public int P4Num
        {
            get { return p4Num; }
            set { p4Num = value; }
        }
        private int p5Num;

        public int P5Num
        {
            get { return p5Num; }
            set { p5Num = value; }
        }
        private int p6Num;

        public int P6Num
        {
            get { return p6Num; }
            set { p6Num = value; }
        }
    }
}
