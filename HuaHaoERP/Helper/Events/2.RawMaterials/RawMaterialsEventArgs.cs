using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    class RawMaterialsEventArgs : EventArgs
    {
        private Model.RawMaterialsModel rawMaterialsData;

        internal Model.RawMaterialsModel RawMaterialsData
        {
            get { return rawMaterialsData; }
            set { rawMaterialsData = value; }
        }
    }
}
