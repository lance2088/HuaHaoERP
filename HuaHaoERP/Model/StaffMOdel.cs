using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Model
{
    class StaffModel
    {
        private Guid guid;

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string jobs;

        public string Jobs
        {
            get { return jobs; }
            set { jobs = value; }
        }
        private DateTime entryTime;

        public DateTime EntryTime
        {
            get { return entryTime; }
            set { entryTime = value; }
        }
        private string Contact;

        public string Contact1
        {
            get { return Contact; }
            set { Contact = value; }
        }
        private int iDNumber;

        public int IDNumber
        {
            get { return iDNumber; }
            set { iDNumber = value; }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private string departureTime;

        public string DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }
        private DateTime deleteMark;

        public DateTime DeleteMark
        {
            get { return deleteMark; }
            set { deleteMark = value; }
        }
        private DateTime addTime;

        public DateTime AddTime
        {
            get { return addTime; }
            set { addTime = value; }
        }
    }
}
