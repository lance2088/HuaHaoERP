
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
