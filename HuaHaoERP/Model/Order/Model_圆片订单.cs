using HuaHaoERP.Model.MeansOfProduction;
using System;

namespace HuaHaoERP.Model.Order
{
    class Model_圆片订单 : Model_圆片资料
    {
        public int OrderType { get; set; }
        public string 单号 { get; set; }
        public string 下单日期 { get; set; }
        public Int64 数量 { get; set; }
        public string 备注 { get; set; }
    }
}
