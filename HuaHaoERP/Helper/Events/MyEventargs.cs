using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    class MyEventArgs : EventArgs
    {
        private int intArgs;
        public int IntArgs
        {
            get { return intArgs; }
            set { intArgs = value; }
        }

        private string stringArgs;
        public string StringArgs
        {
            get { return stringArgs; }
            set { stringArgs = value; }
        }

        private decimal decimalArgs;
        public decimal DecimalArgs
        {
            get { return decimalArgs; }
            set { decimalArgs = value; }
        }

        private List<string> listStrArgs;
        public List<string> ListStrArgs
        {
            get { return listStrArgs; }
            set { listStrArgs = value; }
        }

    }
}
