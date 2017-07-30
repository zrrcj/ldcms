using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.Model
    {
    //dt_group_info
    public class dt_group_info
        {

        /// <summary>
        /// id
        /// </summary>		
        private int _id;
        public int id
            {
            get { return _id; }
            set { _id = value; }
            }
        /// <summary>
        /// mid
        /// </summary>		
        private int _mid;
        public int mid
            {
            get { return _mid; }
            set { _mid = value; }
            }
        /// <summary>
        /// gid
        /// </summary>		
        private int _gid;
        public int gid
            {
            get { return _gid; }
            set { _gid = value; }
            }
        /// <summary>
        /// add_time
        /// </summary>		
        private string _add_time;
        public string add_time
            {
            get { return _add_time; }
            set { _add_time = value; }
            }

        }
    }

