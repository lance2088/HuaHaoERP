using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    class ProcessorsEventArgs : EventArgs
    {
        private Model.ProcessorsModel processorsData;

        internal Model.ProcessorsModel ProcessorsData
        {
            get { return processorsData; }
            set { processorsData = value; }
        }
    }
}
