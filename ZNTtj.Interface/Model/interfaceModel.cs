using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZNTtj.Interface.Model
{
    public class interfaceModel
    {
        public interfaceModel() { }
        #region Model
        private int _paramenc = 0;
        private string _method;
        private string _invokerate;
        private string _invokespace;
        private int _isblack;
        private int _islog;
        /// <summary>
        /// 是否开启日志
        /// </summary>
        public int islog
        {
            get { return _islog; }
            set { _islog = value; }
        }
        /// <summary>
        /// 是否开启黑名单
        /// </summary>
        public int isblack
        {
            get { return _isblack; }
            set { _isblack = value; }
        }
        /// <summary>
        /// 调用时间间隔，单位秒
        /// </summary>
        public string invokespace
        {
            get { return _invokespace; }
            set { _invokespace = value; }
        }
        private string _enckey;

        /// <summary>
        /// 是否加密
        /// </summary>
        public int paramenc
        {
            set { _paramenc = value; }
            get { return _paramenc; }
        }
        /// <summary>
        /// 调用频率
        /// </summary>
        public string invokerate
        {
            get { return _invokerate; }
            set { _invokerate = value; }
        }
        /// <summary>
        /// 加密Key
        /// </summary>
        public string enckey
        {
            set { _enckey = value; }
            get { return _enckey; }
        }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string method
        {
            get { return _method; }
            set { _method = value; }
        }
        #endregion
    }
}