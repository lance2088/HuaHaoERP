using System;
using System.ComponentModel;

namespace HuaHaoERP.Model.ProductionManagement
{
    class Model_ProductionBookkeeping : INotifyPropertyChanged
    {

        private string _DisPlayIsTurn;

        public string DisPlayIsTurn
        {
            get { return _DisPlayIsTurn; }
            set { _DisPlayIsTurn = value; }
        }

        private int _isTurn;

        public int IsTurn
        {
            get { return _isTurn; }
            set { _isTurn = value; NotifyPropertyChanged("IsTurn"); }
        }

        private Guid guid;

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; NotifyPropertyChanged("Guid"); }
        }
        private string orderNum;

        public string OrderNum
        {
            get { return orderNum; }
            set { orderNum = value; NotifyPropertyChanged("OrderNum"); }
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
        private string[] productProcess = new string[6];

        public string[] ProductProcess
        {
            get { return productProcess; }
            set { productProcess = value; NotifyPropertyChanged("ProductProcess"); }
        }
        private string productProcessStr;

        public string ProductProcessStr
        {
            get { return productProcessStr; }
            set { productProcessStr = value; NotifyPropertyChanged("ProductProcessStr"); }
        }
        private int p1Num;

        public int P1Num
        {
            get { return p1Num; }
            set { p1Num = value; NotifyPropertyChanged("P1Num"); }
        }
        private int p1Diff;

        public int P1Diff
        {
            get { return p1Diff; }
            set { p1Diff = value; NotifyPropertyChanged("P1Diff"); }
        }
        private int p2Num;

        public int P2Num
        {
            get { return p2Num; }
            set { p2Num = value; NotifyPropertyChanged("P2Num"); }
        }
        private int p2Diff;

        public int P2Diff
        {
            get { return p2Diff; }
            set { p2Diff = value; NotifyPropertyChanged("P2Diff"); }
        }
        private int p3Num;

        public int P3Num
        {
            get { return p3Num; }
            set { p3Num = value; NotifyPropertyChanged("P3Num"); }
        }
        private int p3Diff;

        public int P3Diff
        {
            get { return p3Diff; }
            set { p3Diff = value; NotifyPropertyChanged("P3Diff"); }
        }
        private int p4Num;

        public int P4Num
        {
            get { return p4Num; }
            set { p4Num = value; NotifyPropertyChanged("P4Num"); }
        }
        private int p4Diff;

        public int P4Diff
        {
            get { return p4Diff; }
            set { p4Diff = value; NotifyPropertyChanged("P4Diff"); }
        }
        private int p5Num;

        public int P5Num
        {
            get { return p5Num; }
            set { p5Num = value; NotifyPropertyChanged("P5Num"); }
        }
        private int p5Diff;

        public int P5Diff
        {
            get { return p5Diff; }
            set { p5Diff = value; NotifyPropertyChanged("P5Diff"); }
        }
        private int p6Num;

        public int P6Num
        {
            get { return p6Num; }
            set { p6Num = value; NotifyPropertyChanged("P6Num"); }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; NotifyPropertyChanged("Remark"); }
        }
        private string addDate;

        public string AddDate
        {
            get { return addDate; }
            set { addDate = value; NotifyPropertyChanged("AddDate"); }
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

    class Model_Product
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
    }
}
