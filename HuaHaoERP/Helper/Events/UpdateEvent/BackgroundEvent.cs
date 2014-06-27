using System;

namespace HuaHaoERP.Helper.Events.UpdateEvent
{
    class BackgroundEvent
    {
        public static EventHandler EUpdateBackground;

        internal static void OnUpdateBackground()
        {
            if (EUpdateBackground != null)
            {
                EUpdateBackground(null, null);
            }
        }

        public static EventHandler EUpdateLoginBackground;

        internal static void OnUpdateLoginBackground()
        {
            if (EUpdateLoginBackground != null)
            {
                EUpdateLoginBackground(null, null);
            }
        }
    }
}
