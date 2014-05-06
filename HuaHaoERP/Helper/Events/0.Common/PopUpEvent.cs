using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    static class PopUpEvent
    {
        internal static EventHandler<PopUpEventArgs> EShowPopUp;
        internal static void OnShowPopUp(object sender, object PageClass)
        {
            if (EShowPopUp != null)
            {
                PopUpEventArgs ee = new PopUpEventArgs();
                ee.ClassObject = PageClass;
                EShowPopUp(sender, ee);
            }
        }

        internal static EventHandler<PopUpEventArgs> EHidePopUp;
        internal static void OnHidePopUp(object sender)
        {
            if (EHidePopUp != null)
            {
                EHidePopUp(sender, new PopUpEventArgs());
            }
        }
    }
}
