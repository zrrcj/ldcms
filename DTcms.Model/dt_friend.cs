using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.Model
    {
    //dt_friend
    public class dt_friend
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
        /// friend_groupname
        /// </summary>		
        private string _friend_groupname;
        public string friend_groupname
            {
            get { return _friend_groupname; }
            set { _friend_groupname = value; }
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
        /// <summary>
        /// mid
        /// </summary>		
        private int _mid;
        public int mid
            {
            get { return _mid; }
            set { _mid = value; }
            }

        }
    }

