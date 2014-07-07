using System;
using System.ComponentModel;

namespace HuaHaoERP.Model.Order
{
    public class Model_BatchInputOrder : INotifyPropertyChanged
    {
        private Guid guid;

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; NotifyPropertyChanged("Guid"); }
        }
        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; NotifyPropertyChanged("Number"); }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged("Name"); }
        }
        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; NotifyPropertyChanged("Date"); }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; NotifyPropertyChanged("Remark"); }
        }
        private string orderType;

        public string OrderType
        {
            get { return orderType; }
            set { orderType = value; NotifyPropertyChanged("OrderType"); }
        }
        private Guid processorsID;

        public Guid ProcessorsID
        {
            get { return processorsID; }
            set { processorsID = value; NotifyPropertyChanged("ProcessorsID"); }
        }
        private string processorsName;

        public string ProcessorsName
        {
            get { return processorsName; }
            set { processorsName = value; NotifyPropertyChanged("ProcessorsName"); }
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
