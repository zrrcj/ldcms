using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.Model
    {
    //dt_group
    public class dt_group
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
        /// groupname
        /// </summary>		
        private string _groupname;
        public string groupname
            {
            get { return _groupname; }
            set { _groupname = value; }
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
        /// avatar
        /// </summary>		
        private string _avatar;
        public string avatar
            {
            get { return _avatar; }
            set { _avatar = value; }
            }

        }
    }

