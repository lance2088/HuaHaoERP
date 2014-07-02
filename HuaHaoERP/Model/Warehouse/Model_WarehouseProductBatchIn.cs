using System;
using System.ComponentModel;

namespace HuaHaoERP.Model.Warehouse
{
    class Model_WarehouseProductBatchIn : INotifyPropertyChanged
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
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
        private string material;

        public string Material
        {
            get { return material; }
            set { material = value; NotifyPropertyChanged("Material"); }
        }
        private int packQuantity;

        public int PackQuantity
        {
            get { return packQuantity; }
            set { packQuantity = value; NotifyPropertyChanged("PackQuantity"); }
        }
        private int perQuantity;

        public int PerQuantity
        {
            get { return perQuantity; }
            set { perQuantity = value; NotifyPropertyChanged("PerQuantity"); }
        }
        private int allQuantity;

        public int AllQuantity
        {
            get { return allQuantity; }
            set { allQuantity = value; NotifyPropertyChanged("AllQuantity"); }
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
