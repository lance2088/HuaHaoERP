using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events.Customer
{
    static class CustomerEvent
    {
        public static EventHandler<CustomerEventArgs> EAdd;
        public static EventHandler<CustomerEventArgs> EDelete;
        public static EventHandler<CustomerEventArgs> EModify;

        public static void OnAdd(object sender, CustomerEventArgs e)
        {
            if(EAdd != null)
            {
                EAdd(sender, e);
            }
        }
        public static void OnDelete(object sender, CustomerEventArgs e)
        {
            if (EDelete != null)
            {
                EDelete(sender, e);
            }
        }
        public static void OnModify(object sender, CustomerEventArgs e)
        {
            if (EModify != null)
            {
                EModify(sender, e);
            }
        }
    }
}
