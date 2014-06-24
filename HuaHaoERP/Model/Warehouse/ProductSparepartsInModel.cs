using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model.Warehouse
{
    class ProductSparepartsInModel
    {
        private Guid guid;

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
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
        private string material;

        public string Material
        {
            get { return material; }
            set { material = value; }
        }
        private int packQuantity;

        public int PackQuantity
        {
            get { return packQuantity; }
            set { packQuantity = value; }
        }
        private int perQuantity;

        public int PerQuantity
        {
            get { return perQuantity; }
            set { perQuantity = value; }
        }
        private int allQuantity;

        public int AllQuantity
        {
            get { return allQuantity; }
            set { allQuantity = value; }
        }
    }
}
