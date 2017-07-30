using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZNTtj.Interface.Model
{
    public class zn_interface_log
    {
        public zn_interface_log()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _clientip;
        private int _clienttype;
        /// <summary>
        /// 客户端类型 1 UC 2 android  3 ios  0 PC 
        /// </summary>
        public int clienttype
        {
            get { return _clienttype; }
            set { _clienttype = value; }
        }
        private string _clientversion;
        private int _responsecode;
        private string _requestinfo;
        private DateTime _begintime;
        private DateTime _endtime;
        private int? _timespans;
        private string _responseinfo;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 调用者IP
        /// </summary>
        public string clientip
        {
            set { _clientip = value; }
            get { return _clientip; }
        }
        /// <summary>
        /// 客户端系统版本
        /// </summary>
        public string clientversion
        {
            set { _clientversion = value; }
            get { return _clientversion; }
        }
        /// <summary>
        /// 返回结果代码
        /// </summary>
        public int responsecode
        {
            set { _responsecode = value; }
            get { return _responsecode; }
        }
        /// <summary>
        /// 参数
        /// </summary>
        public string requestinfo
        {
            set { _requestinfo = value; }
            get { return _requestinfo; }
        }
        /// <summary>
        /// 开始时间 
        /// </summary>
        public DateTime begintime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endtime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 调用时长,单位
        /// </summary>
        public int? timespans
        {
            set { _timespans = value; }
            get { return _timespans; }
        }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string responseinfo
        {
            set { _responseinfo = value; }
            get { return _responseinfo; }
        }
        #endregion Model
    }
}