using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model.Statement
{
    class Model_Processors
    {
        private int _row;
        private string _outDate;
        private string _inDate;
        private string _productName;
        private int _out;
        private int _in;
        private int _inMinorInjuries;
        private int _inInjuries;
        private int _inLose;

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }
        public string OutDate
        {
            get { return _outDate; }
            set { _outDate = value; }
        }
        public string InDate
        {
            get { return _inDate; }
            set { _inDate = value; }
        }
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }
        public int Out
        {
            get { return _out; }
            set { _out = value; }
        }
        public int In
        {
            get { return _in; }
            set { _in = value; }
        }
        public int Difference
        {
            get { return _out - (_in + _inMinorInjuries + _inInjuries + _inLose); }
        }
        public int InMinorInjuries
        {
            get { return _inMinorInjuries; }
            set { _inMinorInjuries = value; }
        }
        public int InInjuries
        {
            get { return _inInjuries; }
            set { _inInjuries = value; }
        }
        public int InLose
        {
            get { return _inLose; }
            set { _inLose = value; }
        }
    }
}