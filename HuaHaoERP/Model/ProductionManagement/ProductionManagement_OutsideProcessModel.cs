using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model
{
    class ProductionManagement_OutsideProcessModel
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
        private string orderDate;

        public string OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }
        private Guid productGuid;

        public Guid ProductGuid
        {
            get { return productGuid; }
            set { productGuid = value; }
        }
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private Guid processorsGuid;

        public Guid ProcessorsGuid
        {
            get { return processorsGuid; }
            set { processorsGuid = value; }
        }
        private string processorsName;

        public string ProcessorsName
        {
            get { return processorsName; }
            set { processorsName = value; }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        private int minorInjuries;

        public int MinorInjuries
        {
            get { return minorInjuries; }
            set { minorInjuries = value; }
        }
        private int injuries;

        public int Injuries
        {
            get { return injuries; }
            set { injuries = value; }
        }
        private int lose;

        public int Lose
        {
            get { return lose; }
            set { lose = value; }
        }
        private string orderType;

        public string OrderType
        {
            get { return orderType; }
            set { orderType = value; }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}
