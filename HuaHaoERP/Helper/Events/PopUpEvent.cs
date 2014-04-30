using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    static class PopUpEvent
    {
        internal static EventHandler<PopUpEventArgs> EShowPopUp;
        internal static void OnShowPopUp(object sender, PopUpEventArgs e)
        {
            if (EShowPopUp != null)
            {
                EShowPopUp(sender, e);
            }
        }

        internal static EventHandler<PopUpEventArgs> EHidePopUp;
        internal static void OnHidePopUp(object sender, PopUpEventArgs e)
        {
            if (EHidePopUp != null)
            {
                EHidePopUp(sender, e);
            }
        }
    }
}
