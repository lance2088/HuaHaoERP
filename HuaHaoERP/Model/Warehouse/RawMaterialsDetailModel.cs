using System;
using System.ComponentModel;

namespace HuaHaoERP.Model
{
    class RawMaterialsDetailModel : INotifyPropertyChanged
    {
        private Guid guid;

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; NotifyPropertyChanged("Guid"); }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; NotifyPropertyChanged("Id"); }
        }
        private decimal number;

        public decimal Number
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
        private Guid rawMaterialsID;

        public Guid RawMaterialsID
        {
            get { return rawMaterialsID; }
            set { rawMaterialsID = value; NotifyPropertyChanged("RawMaterialsID"); }
        }
        private string weight;

        public string Weight
        {
            get { return weight; }
            set { weight = value; NotifyPropertyChanged("Weight"); }
        }

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; NotifyPropertyChanged("Remark"); }
        }

        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; NotifyPropertyChanged("Date"); }
        }

        private string optor;

        public string Operator
        {
            get { return optor; }
            set { optor = value; NotifyPropertyChanged("Operator"); }
        }

        private string amount;

        public string Amount
        {
            get { return amount; }
            set { amount = value; NotifyPropertyChanged("Amount"); }
        }

        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; NotifyPropertyChanged("Code"); }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; NotifyPropertyChanged("Type"); }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private Helpers

        /// <summary>
        /// cell内容改变事件
        /// </summary>
        /// <param name="propertyName"></param>
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
