using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace HuaHaoERP.Model.ProductionManagement
{
    class Model_ProductionManagement_OutsideProcessBatch : INotifyPropertyChanged
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private Guid productGuid;

        public Guid ProductGuid
        {
            get { return productGuid; }
            set { productGuid = value; NotifyPropertyChanged("ProductGuid"); }
        }
        private string productNumber;

        public string ProductNumber
        {
            get { return productNumber; }
            set { productNumber = value; NotifyPropertyChanged("ProductNumber"); }
        }
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; NotifyPropertyChanged("ProductName"); }
        }
        private string material;

        public string Material
        {
            get { return material; }
            set { material = value; NotifyPropertyChanged("Material"); }
        }
        private Guid processorsGuid;

        public Guid ProcessorsGuid
        {
            get { return processorsGuid; }
            set { processorsGuid = value; NotifyPropertyChanged("ProcessorsGuid"); }
        }
        private string processorsNumber;

        public string ProcessorsNumber
        {
            get { return processorsNumber; }
            set { processorsNumber = value; NotifyPropertyChanged("ProcessorsNumber"); }
        }
        private string processorsName;

        public string ProcessorsName
        {
            get { return processorsName; }
            set { processorsName = value; NotifyPropertyChanged("ProcessorsName"); }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; NotifyPropertyChanged("Quantity"); }
        }
        private int minorInjuries;

        public int MinorInjuries
        {
            get { return minorInjuries; }
            set { minorInjuries = value; NotifyPropertyChanged("MinorInjuries"); }
        }
        private int injuries;

        public int Injuries
        {
            get { return injuries; }
            set { injuries = value; NotifyPropertyChanged("Injuries"); }
        }
        private int lose;

        public int Lose
        {
            get { return lose; }
            set { lose = value; NotifyPropertyChanged("Lose"); }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; NotifyPropertyChanged("Remark"); }
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
