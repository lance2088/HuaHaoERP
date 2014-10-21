using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model.ProductionManagement
{
    class ProductManagement_PickingDetailModel : INotifyPropertyChanged
    {
        private int _id;
        private Guid _guid;
        private string _number;
        private string _name;
        private Guid _productID;
        private int quantityA;
        private int quantityB;
        private int quantityC;
        private int quantityD;

        public int QuantityD
        {
            get { return quantityD; }
            set { quantityD = value; NotifyPropertyChanged("QuantityD"); }
        }

        public int QuantityC
        {
            get { return quantityC; }
            set { quantityC = value; NotifyPropertyChanged("QuantityC"); }
        }

        public int QuantityB
        {
            get { return quantityB; }
            set { quantityB = value; NotifyPropertyChanged("QuantityB"); }
        }

        public int QuantityA
        {
            get { return quantityA; }
            set { quantityA = value; NotifyPropertyChanged("QuantityA"); }
        }


        public Guid ProductID
        {
            get { return _productID; }
            set { _productID = value; NotifyPropertyChanged("ProductID"); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name"); }
        }

        public string Number
        {
            get { return _number; }
            set { _number = value; NotifyPropertyChanged("Number"); }
        }

        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value;}
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
