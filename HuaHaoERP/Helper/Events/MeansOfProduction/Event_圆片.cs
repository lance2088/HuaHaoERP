using System;

namespace HuaHaoERP.Helper.Events.MeansOfProduction
{
    class Event_圆片
    {
        public static EventHandler EUpdate圆片资料;

        internal static void OnUpdate圆片资料()
        {
            if (EUpdate圆片资料 != null)
            {
                EUpdate圆片资料(null, null);
            }
        }

        public static EventHandler EUpdate圆片库存;

        internal static void OnUpdate圆片库存()
        {
            if (EUpdate圆片库存 != null)
            {
                EUpdate圆片库存(null, null);
            }
        }

        public static EventHandler EUpdate圆片订单;

        internal static void OnUpdate圆片订单()
        {
            if (EUpdate圆片订单 != null)
            {
                EUpdate圆片订单(null, null);
            }
        }
    }
}
