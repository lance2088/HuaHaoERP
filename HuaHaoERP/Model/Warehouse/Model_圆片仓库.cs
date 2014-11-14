using HuaHaoERP.Model.MeansOfProduction;
using System;

namespace HuaHaoERP.Model.Warehouse
{
    class Model_圆片仓库 : Model_圆片资料
    {
        private string _入库半成品编号;
        private string _半成品品名;

        public Int64 数量 { get; set; }
        public Int64 损耗数量 { get; set; }
        public Guid 半成品Guid { get; set; }
        public string 入库半成品编号
        {
            get { return _入库半成品编号; }
            set { _入库半成品编号 = value; NotifyPropertyChanged("入库半成品编号"); }
        }
        public string 半成品品名
        {
            get { return _半成品品名; }
            set { _半成品品名 = value; NotifyPropertyChanged("半成品品名"); }
        }
        public Int64 半成品数量 { get; set; }
    }
}
