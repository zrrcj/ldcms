using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.Model
    {
    //dt_msg_record
    public class dt_msg_record
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
        /// 发送人id
        /// </summary>		
        private int _uid;
        public int uid
            {
            get { return _uid; }
            set { _uid = value; }
            }
        /// <summary>
        /// 接收人id当为群消息时该值为群id
        /// </summary>		
        private int _toid;
        public int toid
            {
            get { return _toid; }
            set { _toid = value; }
            }
        /// <summary>
        /// 发送内容
        /// </summary>		
        private string _content;
        public string content
            {
            get { return _content; }
            set { _content = value; }
            }
        /// <summary>
        /// 添加时间
        /// </summary>		
        private string _add_time;
        public string add_time
            {
            get { return _add_time; }
            set { _add_time = value; }
            }
        /// <summary>
        /// 0未读1已读
        /// </summary>		
        private int _status;
        public int status
            {
            get { return _status; }
            set { _status = value; }
            }
        /// <summary>
        /// 0好友信息1群消息
        /// </summary>		
        private int _isgroupmsg;
        public int isgroupmsg
            {
            get { return _isgroupmsg; }
            set { _isgroupmsg = value; }
            }

        }
    }

