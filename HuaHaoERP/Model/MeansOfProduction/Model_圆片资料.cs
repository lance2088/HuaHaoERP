using System;
using System.ComponentModel;

namespace HuaHaoERP.Model.MeansOfProduction
{
    class Model_圆片资料 : INotifyPropertyChanged
    {
        private string _编号;
        private string _直径;
        private string _厚度;

        public Guid Guid { get; set; }
        public int 序号 { get; set; }
        public string 编号
        {
            get { return _编号; }
            set { _编号 = value; NotifyPropertyChanged("编号"); }
        }
        public string 直径
        {
            get { return _直径; }
            set { _直径 = value; NotifyPropertyChanged("直径"); }
        }
        public string 厚度
        {
            get { return _厚度; }
            set { _厚度 = value; NotifyPropertyChanged("厚度"); }
        }
        public string 备注 { get; set; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
