using System;

namespace HuaHaoERP.Model
{
    class RawMaterialsDetailModel
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
        private decimal number;

        public decimal Number
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
        private Guid rawMaterialsID;

        public Guid RawMaterialsID
        {
            get { return rawMaterialsID; }
            set { rawMaterialsID = value; }
        }
        private string weight;

        public string Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        private string optor;

        public string Operator
        {
            get { return optor; }
            set { optor = value; }
        }

        private string amount;

        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
