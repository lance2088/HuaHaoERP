using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.AppInitialize
{
    static class Initialize
    {
        internal static void Init()
        {
            ViewModel.EventDistribution.InitEvent();
        }
    }
}
