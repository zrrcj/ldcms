using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.Model
    {
    //dt_manager_info
    public class dt_manager_info
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
        /// name
        /// </summary>		
        private string _name;
        public string name
            {
            get { return _name; }
            set { _name = value; }
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
        /// <summary>
        /// sign
        /// </summary>		
        private string _sign;
        public string sign
            {
            get { return _sign; }
            set { _sign = value; }
            }

        }
    }

