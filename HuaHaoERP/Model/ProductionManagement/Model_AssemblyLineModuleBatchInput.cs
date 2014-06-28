using System;
using System.ComponentModel;

namespace HuaHaoERP.Model.ProductionManagement
{
    class Model_AssemblyLineModuleBatchInput : INotifyPropertyChanged
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
        private string process;

        public string Process
        {
            get { return process; }
            set { process = value; NotifyPropertyChanged("Process"); }
        }
        private Guid staffGuid;

        public Guid StaffGuid
        {
            get { return staffGuid; }
            set { staffGuid = value; NotifyPropertyChanged("StaffGuid"); }
        }
        private string staffNumber;

        public string StaffNumber
        {
            get { return staffNumber; }
            set { staffNumber = value; NotifyPropertyChanged("StaffNumber"); }
        }
        private string staffName;

        public string StaffName
        {
            get { return staffName; }
            set { staffName = value; NotifyPropertyChanged("StaffName"); }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; NotifyPropertyChanged("Quantity"); }
        }
        private int injure;

        public int Injure
        {
            get { return injure; }
            set { injure = value; NotifyPropertyChanged("Injure"); }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; NotifyPropertyChanged("Remark"); }
        }
        private string[] processList = new string[6];

        public string[] ProcessList
        {
            get { return processList; }
            set { processList = value; }
        }
        private string processListStr;

        public string ProcessListStr
        {
            get { return processListStr; }
            set { processListStr = value; NotifyPropertyChanged("ProcessListStr"); }
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
