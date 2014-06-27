using System;

namespace HuaHaoERP.Model
{
    class ScrapModel
    {
        private Guid guid;

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        private string opt;

        public string Operator
        {
            get { return opt; }
            set { opt = value; }
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

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}
