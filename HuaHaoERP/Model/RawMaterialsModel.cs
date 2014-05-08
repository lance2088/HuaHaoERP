using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model
{
    class RawMaterialsModel
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
        private string weight;

        public string Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        private string material;

        public string Material
        {
            get { return material; }
            set { material = value; }
        }
        private string supplierNumber;

        public string SupplierNumber
        {
            get { return supplierNumber; }
            set { supplierNumber = value; }
        }
        private string incomingDate;

        public string IncomingDate
        {
            get { return incomingDate; }
            set { incomingDate = value; }
        }
        private string sp1;

        public string Sp1
        {
            get { return sp1; }
            set { sp1 = value; }
        }
        private string sp2;

        public string Sp2
        {
            get { return sp2; }
            set { sp2 = value; }
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
