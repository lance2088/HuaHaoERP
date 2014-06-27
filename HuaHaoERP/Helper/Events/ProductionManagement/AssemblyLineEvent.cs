using System;
using System.Collections.Generic;

namespace HuaHaoERP.Helper.Events
{
    class AssemblyLineEvent
    {
        internal static EventHandler<AssemblyLineEventArgs> ERemoveAssemblyLineModule;

        internal static void OnRemoveAssemblyLineModule(string RegisterName)
        {
            if(ERemoveAssemblyLineModule != null)
            {
                AssemblyLineEventArgs e = new AssemblyLineEventArgs();
                e.RegisterName = RegisterName;
                ERemoveAssemblyLineModule(null, e);
            }
        }

        internal static EventHandler<AssemblyLineEventArgs> EShowAssemblyLineModule;
        internal static void OnShowAssemblyLineModule(List<Model.ProductModel> d)
        {
            if(EShowAssemblyLineModule != null)
            {
                AssemblyLineEventArgs e = new AssemblyLineEventArgs();
                e.ProductData = d;
                EShowAssemblyLineModule(null, e);
            }
        }
    }
}
