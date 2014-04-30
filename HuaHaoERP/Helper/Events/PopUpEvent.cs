using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    static class PopUpEvent
    {
        public static EventHandler<PopUpEventArgs> EShowPopUp;
        public static void OnShowPopUp(object sender, PopUpEventArgs e)
        {
            if (EShowPopUp != null)
            {
                EShowPopUp(sender, e);
            }
        }

        public static EventHandler<PopUpEventArgs> EHidePopUp;
        public static void OnHidePopUp(object sender, PopUpEventArgs e)
        {
            if (EHidePopUp != null)
            {
                EHidePopUp(sender, e);
            }
        }
    }
}
