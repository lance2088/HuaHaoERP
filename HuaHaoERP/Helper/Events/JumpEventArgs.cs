using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    class JumpEventArgs
    {
        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        private int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }
    }
}
