using HuaHaoERP.Model.Warehouse;
using System;
using System.Collections.Generic;

namespace HuaHaoERP.Model.Order
{
    class Model_圆片订单
    {
        public Model_圆片订单()
        {
            明细 = new List<Model_圆片仓库>();
        }

        public Guid Guid { get; set; }
        public int OrderType { get; set; }
        public int 序号 { get; set; }
        public string 单号 { get; set; }
        public string 下单日期 { get; set; }
        public string 备注 { get; set; }
        public List<Model_圆片仓库> 明细 { get; set; }
        //供订单列表显示用的明细
        public string 编号s { get; set; }
        public string 直径s { get; set; }
        public string 厚度s { get; set; }
        public string 数量s { get; set; }
    }
}
