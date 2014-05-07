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
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
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
        private string entryTime;

        public string EntryTime
        {
            get { return entryTime; }
            set { entryTime = value; }
        }
        private string seniority;
        /// <summary>
        /// 工龄
        /// </summary>
        public string Seniority
        {
            get 
            { 
                return seniority; 
            }
            set { seniority = value; }
        }
        private string contact;

        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }
        private string iDNumber;

        public string IDNumber
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
