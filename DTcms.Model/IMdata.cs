using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.Model
    {
    public class Mine
        {
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }
        }

    public class ListItem
        {
        /// <summary>
        /// 好友名称
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 好友id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 好友头像
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        }

    public class FriendItem
        {
        /// <summary>
        /// 好友分组名称
        /// </summary>
        public string groupname { get; set; }
        /// <summary>
        /// 分组id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int online { get; set; }
        /// <summary>
        /// 好友列表
        /// </summary>
        public List<ListItem> list { get; set; }
        }

    public class GroupItem
        {
        /// <summary>
        /// 群名称
        /// </summary>
        public string groupname { get; set; }
        /// <summary>
        /// 群id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 群头像
        /// </summary>
        public string avatar { get; set; }
        }

    public class Data
        {
        /// <summary>
        /// 用户信息
        /// </summary>
        public Mine mine { get; set; }
        /// <summary>
        /// 好友列表
        /// </summary>
        public List<FriendItem> friend { get; set; }
        /// <summary>
        /// 群列表
        /// </summary>
        public List<GroupItem> group { get; set; }
        }

    public class Root
        {
        /// <summary>
        /// 返回状态
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// IM数据
        /// </summary>
        public Data data { get; set; }
        }
    }
    