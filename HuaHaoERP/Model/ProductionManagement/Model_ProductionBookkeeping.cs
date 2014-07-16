using System;
using System.ComponentModel;

namespace HuaHaoERP.Model.ProductionManagement
{
    class Model_ProductionBookkeeping : INotifyPropertyChanged
    {
        private Guid guid;
        private Guid productGuid;
        private string productNumber;
        private string productName;
        private string[] productProcess = new string[6];
        private string productProcessStr;
        private int p1Num;
        private int p2Num;
        private int p3Num;
        private int p4Num;
        private int p5Num;
        private int p6Num;
        private string remark;

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
