using System;
namespace ZNTtj.Interface.Model
{
    /// <summary>
    /// zn_interface_blacklist:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class zn_interface_blacklist
    {
        public zn_interface_blacklist()
        { }
        #region Model
        private int _id;
        private string _ip;
        private DateTime _addtime;
        private int _isvalid;
        private int _count;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// IP
        /// </summary>
        public string ip
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int isvalid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
        }
        /// <summary>
        /// 加过几次黑名单
        /// </summary>
        public int count
        {
            set { _count = value; }
            get { return _count; }
        }
        #endregion Model

    }
}